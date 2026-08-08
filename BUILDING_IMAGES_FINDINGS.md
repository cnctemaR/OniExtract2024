# Building Images — Diagnostic Findings

Notes from the post-ship debugging session. Read before touching
`BuildingImageSnapshotter.cs` or `ExportBuildingImages.cs`.

---

## Current state (commit c9eb612)

The "Export Building Images" pass works end-to-end and completes without crashing.
All `ShowInBuildMenu && !Deprecated` buildings are rendered at 200 px/cell and
written to `ui_image/{prefabId}.png`. Two specific buildings the website shows still
have outstanding issues — see below.

---

## What the website actually does with ui_image/ icons

From `WEBSITE_POSTPROCESSING.md`:

> *"We scale every icon to the building footprint regardless of the file's pixel
> size."*

The website scales each icon to `widthInCells × heightInCells × 100 px/cell` on
screen. This means **image aspect ratio must match the building footprint** or the
icon will appear squished/stretched. If the image is the correct width but taller
(due to visual overhang below the footprint), the extra height hangs below the
selectable footprint area — that's fine and was the old sprite's behaviour too.

---

## Building name mismatches

The names the website and the player use differ from the prefab IDs in the database.
Confirmed via the exported `building.json`:

| Player name | prefab ID (`building.json` `name`) | cells | status |
|---|---|---|---|
| Steam Turbine (old) | `SteamTurbine` | 5×4 | **deprecated** (`DeprecatedContent` tag) |
| Steam Turbine | `SteamTurbine2` | 5×3 | current, non-deprecated |
| Steam Engine | `SteamEngineCluster` | 7×5 | current |
| Auto-Sweeper | `SolidTransferArm` | 3×1 | current, `defaultAnimState = "off"` |
| Sweepy's Dock | `SweepBotStation` | 2×2 | current |

There is no `AutoSweeper` prefab ID. Looking for `AutoSweeper.png` will always miss.

---

## The "ui" animation — do not play it on a spawned building

**Symptom:** Playing `kbac.Play("ui", KAnim.PlayMode.Paused)` before snapshotting
caused all exported images to shrink to ~100 px/cell instead of 200 px/cell.
`SolidTransferArm` came out 288×383 px when it should be ~600×200 px. All 1,239
files in the export ended up under 400 px wide.

**Why:** The `"ui"` KAnim animation is Klei's small icon animation. Its frames are
drawn in world-space at the same scale the build-menu atlas bakes them (~100 px/cell
equivalent in world coordinates). Rendering it through our 200 px/cell camera
captures those small frames and gives us atlas-sprite-sized output, not live-kanim
output. The `"ui"` animation is useful for the sprite-extraction path
(`Def.GetUISpriteFromMultiObjectAnim`) but is wrong for live camera rendering.

**Rule:** Never call `kbac.Play("ui")` before a camera snapshot.

---

## Deprecated buildings

Our filter `def.Deprecated || !def.ShowInBuildMenu` correctly skips `SteamTurbine`
(which carries the `DeprecatedContent` tag). However, the website still displays
deprecated buildings, so `ui_image/SteamTurbine.png` remains the old 144×124 atlas
sprite. The user sees it as a "steam turbine that cuts off a block and a half early."

**Reproduced (2026-06-21):** A run with `def.Deprecated` removed from the filter
crashed again — wrote 65 buildings (through `DevPumpLiquid`) then the game exited
mid-sweep with no managed stack trace (state corruption / native crash, not a
catchable spawn-site exception). Confirmed it is the deprecated content, not a
specific normal building: the *prior* run (with the `Deprecated` filter intact)
rendered the very next def (`DevPumpSolid`) and all 341 buildings cleanly, and the
only category the filter-removed run additionally spawns is deprecated buildings.
(The `CrewCapsuleComplete` / `RocketLaunchConditionVisualizer` null-ref seen in the
log is non-fatal — that building also spawned in the clean prior run.) The exact
deprecated culprit is still not isolated; it spawns in the C/D alphabetical range
and the crash manifests a few buildings later, matching the "corrupts state, then
dies" pattern.

**Fix applied:** Keep the `def.Deprecated` skip, but opt specific deprecated
buildings back in via `ExportBuildingImages.DeprecatedAllowlist` (currently just
`SteamTurbine`). This renders the one deprecated building the website needs hi-res
while skipping the unvetted, crash-prone rest — the "safe approach" below, made
concrete. `SteamTurbine` is a vanilla power building (no rocket / virtual-network
components) so it is expected to spawn cleanly; it sits late ('S') in the sweep, so
if it ever does crash it will show up near the end and can be dropped from the
allowlist. To add more deprecated buildings later, vet each by spawning it in
isolation first.

---

## Active-state posing (PoseActive)

**Problem (reported post-ship):** buildings rendered "drab" / "shut down". Two causes,
one root: we captured an *idle/off state at frame 0*.
- The old logic only switched `defaultAnimState == "off"` → `"on"`; every other building
  was snapshotted in whatever calm state it auto-spawned into (no power/inputs).
- `Play(anim, Paused)` parks on frame 0 — for a `working_loop`/`generating_loop` that is
  the retracted "starting" pose (e.g. `SolidTransferArm`'s arm pulled in).

**Fix:** `BuildingImageSnapshotter.PoseActive` runs right before `SnapShot()` (no
intervening game tick, so a building's own state machine can't revert it):
1. `ChooseActiveAnim` enumerates the build's real animation names —
   `KAnimGroupFile.GetGroup(kbac.GetBuildHash()).animNames` (all public firstpass API,
   no reflection) — and scores them: `working/generating/channeling/dispensing/emitting`
   = 100, `on` = 50, `idle` = 20, `+10` for a `loop`, `−8` for `_pre`/`_pst`; anything
   matching `InactiveMarkers` (`off`, `broken`, `unpowered`, `ui`, …) is rejected.
   The highest strictly-positive score wins; otherwise the building keeps its default.
2. If the anim list can't be enumerated (group lookup keyed by tag returns null), it
   falls back to `ProbeFallback` — a fixed most-active-first list probed via
   `HasAnimation`. So the improvement holds even if enumeration mis-keys for some build.
3. The chosen anim is played `Paused` on the **root controller only**, then seeked with
   `SetPositionPercent(PoseFramePercent)` (default **0.5** = mid-loop, the fully-deployed/
   emitting pose).

**Do NOT drive child controllers.** An earlier revision called `Play`/`SetPositionPercent`
on every `GetComponentsInChildren` controller that had the anim. Child controllers (a
separate arm/base/decorative part) sit at their own transform offsets and rotations, so
forcing them to the main building anim stamped a *full extra copy of the building* at each
child's offset — the snapshots came out as multiple overlapping/rotated building copies.
The render path already draws every child in its own natural state; posing must touch only
the root controller (as the original `Play("on")` via `GetComponent` did).

Never use `"ui"` (icon scale — see above). To tune the look, adjust `PoseFramePercent`
or the `ScoreAnim` weights / `InactiveMarkers` in `BuildingImageSnapshotter.cs`.

---

## Material tint — already captured, no fix needed

Phase 3 flagged a risk that buildings relying on a runtime tint
(`kbac.TintColour` / `kbac.SetSymbolTint`) might render grey/white. Verified
against the decompiled `Assembly-CSharp-firstpass.dll` that this is **not** the
case — the live-batch render path captures both forms of tint:

- `KBatchedAnimController.SetSymbolTint` writes into `symbolInstanceGpuData`;
  `KAnimBatch.WriteSymbolInstanceData` copies it into the batch's
  `symbolInstanceTex`.
- The overall `TintColour` lives in the controller's `animInstanceData.tintColour`
  (`GetBatchInstanceData()`); `KAnimBatch.WriteBatchedAnimInstanceData` copies it
  into the batch's `dataTex`.
- Both textures are bound into `KAnimBatch.matProperties`, and both setters call
  `SetDirty()` → `needsWrite = true`.
- `KAnimBatchManager.UpdateDirty(frame)` performs the flush. The snapshotter calls
  exactly this immediately before `snapshotCamera.Render()` and draws with
  `batch.matProperties`, so any tint the controller has applied is on the GPU data
  by render time.

**Consequence:** reading the tint off the kbac and re-applying it to the same kbac
would be a strict no-op. No code change made. (The verified connection-sprite tool
uses the identical render path with no tint handling and renders utility-building
colours correctly — corroborating evidence.)

**One residual nuance (deferred, not a tint-read bug):** buildings that *derive*
their tint from the construction element (tiles, Tempshift Plate, insulation
variants) render in **Unobtanium's** colour, because the spawn loop builds every
def from `SimHashes.Unobtanium` (same neutral debug element the connection tool
uses). To match the recognizable build-menu colour, those would need to spawn from
their default/primary recipe material instead — a riskier spawn-element change
(element availability, recipe edge cases) that should only be made *after* in-game
QA shows it actually matters and doesn't regress the common case. Left as-is.

---

## Crash history — what component combinations break

| Component on BuildingComplete | Effect when spawned without context | Fix applied |
|---|---|---|
| `WireUtilitySemiVirtualNetworkLink` | Registers null key in `CircuitManager` virtual-network dict; `CircuitManager.Rebuild()` throws every tick until game exits | Now caught by `RocketModuleCluster` filter (broader) |
| `RocketModuleCluster` | `OnSpawn`/`OnCleanUp` null-refs; may corrupt virtual-network state | Filtered by `def.BuildingComplete.TryGetComponent<RocketModuleCluster>()` |
| `ReorderableBuilding` (without `RocketModuleCluster`) | Logs NullRef on spawn but does NOT corrupt game state; safe to continue | No filter needed |
| Unknown deprecated building | Crashed the game when `def.Deprecated` filter was removed | Reverted; root cause unknown |

**The CraftModuleInterface fix:** before the spawn loop, we add
`ClusterDestinationSelector`, `ClusterTraveler`, `Clustercraft`, and
`CraftModuleInterface` to the world GameObject. This gives rocket-module components
an ancestor to bind to so they don't null-ref. Without it, many DLC rocket modules
crash on spawn even with the `RocketModuleCluster` filter in place.

---

## PaddingPx

Current value: **400 px** (2 cells of padding each side at 200 px/cell).

Originally 200 px. Doubling to 400 px did not, on its own, fix the "steam turbine
cuts off early" symptom because the turbine involved was `SteamTurbine`
(deprecated, then skipped entirely — old atlas sprite served), and `SteamTurbine2`
was rendering at icon scale due to the `"ui"` animation bug. With those two bugs
fixed (deprecated filter removed, no `"ui"` play), 400 px gives ~2 cells of
headroom so kanim parts that hang below the footprint (e.g. `SteamTurbine2`'s
dangling pipes) are captured. The opaque-bbox crop removes the empty margin, so the
larger padding does not inflate output size — it only widens the capture window.

---

## uiImageRect — footprint-relative placement for the website

The website stretches each `ui_image/` icon to fill the footprint box. The old atlas
sprites were footprint-shaped, so that was harmless; the hi-res kanim renders tight-crop
to the art's true bounding box, which overhangs the footprint, so a blind stretch squishes
them (steam-turbine exhaust crushed, auto-sweeper squished — see `WEBSITE_POSTPROCESSING.md`).

The fix is to emit, per building, the rendered PNG's rectangle in **cells, relative to the
footprint** — the field the website already consumes:

```json
"uiImageRect": { "x": 0, "y": -1.24, "w": 5, "h": 4.24 }
```

Footprint = (0,0) bottom-left to (W,H) top-right; +x right, **+y up**; `x,y` = the PNG's
bottom-left corner, `w,h` = PNG size. Below-footprint overhang ⇒ negative `y`.

**Where it's computed.** `BuildingImageSnapshotter.ComputeRect` → `UiImageRect.FromCrop`
(pure, unit-tested in `BuildingImageSnapshotterTests`). The snapshot camera centres the
footprint on the texture centre at `PixelsPerCell` px/cell, so for an opaque crop with
bottom-left pixel `(minX, minY)` and size `(cw, ch)`:

```
x = (minX - texW/2)/ppc + W/2
y = (minY - texH/2)/ppc + H/2     // minY is the bottom opaque row (GetPixels is bottom-up)
w = cw/ppc                         // → no manual Y flip; negative y falls out naturally
h = ch/ppc
```

The camera-position terms cancel, so the rect is independent of where the building spawned.
It relies on the existing camera framing being correct: footprint bottom = building
`transform.y`, footprint h-centre = `transform.x` (+0.5 for even widths) — the same
`pos.y += H/2` / `pos.x += 0.5` adjustments `InitCamera` already makes to centre the shot.

**Where it lands.** The rect can only be measured from a live in-game render, long after
the main-menu pass writes `building.json`. So `ExportBuildingImages` collects every rendered
building's rect (keyed by prefab tag name == `building.json` `name`) and, after the sweep,
merges them into `database[_base]/building.json` via JSON.NET (`PatchBuildingJson`). Buildings
we skip (deprecated/rocket modules) get no rect and keep the website's legacy
stretch-to-footprint fallback. Watch the log line
`buildings with uiImageRect placement: N / total` to track coverage — it mirrors the
converter's own log on the website side. Run the main-menu export first; if `building.json`
is missing, the merge logs a warning and no-ops.

## How to diagnose future image issues

1. **Check which prefab ID the website is using** — player names ≠ prefab IDs.
   Look up the name in `export/database/building.json` → `name` field.

2. **Check the PNG dimensions** using the PowerShell snippet:
   ```powershell
   function Get-PngSize($p) {
       $fs = [IO.File]::OpenRead($p); $b = New-Object byte[] 28
       $null = $fs.Read($b,0,28); $fs.Close()
       $w = [Net.IPAddress]::NetworkToHostOrder([BitConverter]::ToInt32($b[16..19],0))
       $h = [Net.IPAddress]::NetworkToHostOrder([BitConverter]::ToInt32($b[20..23],0))
       "${w}x${h}"
   }
   Get-PngSize "...\export\ui_image\SomeBuilding.png"
   ```
   At 200 px/cell a typical 2×3 building should be ~400–700 px wide after bbox-crop.
   Under 400 px almost certainly means the `"ui"` animation was played (icon scale),
   or the building was skipped and the old atlas sprite is still there.

3. **Check the modification timestamp** on the PNG to confirm it came from the
   animation pass (timestamps cluster within ~2 minutes during the in-game export)
   versus the main-menu pass (runs at game load, earlier timestamp).

4. **Check `def.Deprecated`** — if the prefab carries `DeprecatedContent` tag it
   will be skipped by the current filter and the old atlas sprite remains.
