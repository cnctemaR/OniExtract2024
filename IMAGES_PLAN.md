# Plan: High-Res Building Icons via Live-Kanim Camera Snapshot

Working plan for replacing the low-resolution `ui_image/` building icons with crisp renders of
the actual build-complete kanim. Phased so each step is independently shippable and verifiable.
Update the checkboxes as phases land; delete this file once the work is done and folded into
the README.

## Why

The `ui_image/` icons are produced at the **main menu** (`Patches.cs`, the no-save JSON export).
With no game loaded, the only thing available is each building's pre-baked **UI atlas sprite**,
which `ExportUISprite` pulls via `Def.GetUISprite(...)` and `AnimTool` crops out of the atlas
texture. Those atlas sprites are small menu thumbnails — that is the resolution ceiling, and
cropping can't add detail that was never baked in.

The fix (suggested by an experienced modder, ref
`Sgt_Imalas-Oni-Mods/AnimExportTool/AETE_KbacSnapShotter.cs`) is to render the **live kanim**
through a camera into a RenderTexture at a chosen pixels-per-cell, instead of reading the baked
sprite. We already use this exact technique in
[ConnectionSpriteSnapshotter.cs](OniExtract2024/connection/ConnectionSpriteSnapshotter.cs) for the
connection-sprite tool, so the proven machinery (camera clone + batch render + reflection shims
for stock Assembly-CSharp) already lives in this repo.

## Fixed decisions

- **Scope:** buildings only (~449 defs). Elements, items, critters, and facades keep the existing
  main-menu atlas path for now.
- **Resolution:** 200 px/cell (2x today). Well under the website's 5 MB/icon test for any
  realistic building.
- **Runs in-game**, as a new pause-screen button **"Export Building Images"**, mirroring the
  connection-sprite tool. The main-menu JSON+icon export is left fully intact — it still emits
  every icon; this pass *overwrites only the building PNGs* with sharper versions.
- **Filename = identical to the main-menu pass.** Default option `SaveNameMod.ID` writes
  `ui_image/{prefabTag.Name}.png` (the prefabId the website relies on — see
  [WEBSITE_POSTPROCESSING.md](WEBSITE_POSTPROCESSING.md) contract §1). Factor
  `ExportUISprite.GetFormatedUIImageFileName` into a shared helper so the two passes can never
  disagree and the hi-res file lands exactly on top of the low-res one.
- **Framing = tight crop to the opaque bounding box.** Same framing as today, just more pixels →
  no website change ("same framing, more pixels" per WEBSITE_POSTPROCESSING resolution rules).
- **Do not touch the verified connection path.** Write a new self-contained
  `BuildingImageSnapshotter` rather than refactoring `ConnectionSpriteSnapshotter`. Lifting the
  shared batch-render/reflection shims into one helper is deferred cleanup, not on the critical
  path.

## Phases

### Phase 1 — pipeline smoke test
- [x] Add the **"Export Building Images"** pause-screen button (new patch, alongside
      `ConnectionExportPatches`).
- [x] Driver snapshots **one** building (e.g. `ManualGenerator`) at 200 px/cell: spawn off-screen,
      render live kanim, trim to opaque bbox, write `ui_image/{prefabId}.png`.
- [x] Verify in-game: button appears, `Player.log` logs the output path, PNG is crisp and
      correctly cropped.

Proves spawn → camera → trim → write end-to-end before touching 449 buildings.

### Phase 2 — full building sweep
- [x] Iterate `Assets.BuildingDefs`; spawn each off-screen via the connection tool's
      `def.Create(..., def.BuildingComplete)` pattern; snapshot, trim, write, destroy.
- [x] Coroutine with per-building `yield` and progress logging; skip defs that fail to instantiate.
- [x] Deliverable: all building icons regenerated at 200 px/cell.

### Phase 3 — fidelity pass (QA-driven)
- [x] Choose the correct display state per building: `PoseActive` scores each build's real
      animation list and plays the most-active state (working/generating/on) seeked to a
      mid-loop frame, instead of the drab idle/off frame-0 pose; never `"ui"` (icon scale).
      See `BUILDING_IMAGES_FINDINGS.md` "Active-state posing".
- [x] Handle multi-`KBatchedAnimController` buildings (render iterates all child controllers,
      z-ordered) and material tint (live-batch render already captures `TintColour` /
      `SetSymbolTint` — verified against decompiled Assembly-CSharp; see findings doc. No fix).
- [x] Handle outliers: deprecated buildings skipped except a vetted allowlist (`SteamTurbine`) —
      a blanket removal crashes the sweep; `RocketModuleCluster` filtered to avoid crashes;
      padding raised to 400 px for below-footprint kanim parts.
- [x] Emit `uiImageRect` (cells, footprint-relative) per rendered building so the website can
      place tight-cropped icons without squishing overhang. Measured in `UiImageRect.FromCrop`
      (unit-tested), merged into `database/building.json` by `ExportBuildingImages.PatchBuildingJson`.
      See `BUILDING_IMAGES_FINDINGS.md` "uiImageRect" and `WEBSITE_POSTPROCESSING.md`.
- [x] **Validated in-website:** `SteamTurbine2` (below-footprint exhaust → negative `y`) and
      `SolidTransferArm` (`"off"`→`"on"` pose) both render correctly with `uiImageRect`.
- [ ] **Remaining `uiImageRect` spot-checks** — each exercises an untested branch of the rect
      math; pick one representative of each from the website (see findings doc "uiImageRect"):
      1. **Even-width building** (e.g. Coal Generator 2×2, Manual Generator 2×3) — validates the
         `+0.5` horizontal centring. If wrong it shifts ~half a cell sideways: subtle, easy to
         miss, so check alignment against the footprint grid deliberately.
      2. **Side overhang** (art bleeds left/right past the footprint) → non-zero `x` and/or
         `x+w > W`. Confirm horizontal overhang hangs out instead of squishing in.
      3. **Above-footprint overhang** (tall art extending up, `y ≈ 0` but `h > H`) — the upward
         counterpart to the turbine's downward case.
      4. **Plain footprint-filling box** (e.g. a battery/storage bin) — should read
         `x≈0, y≈0, w≈W, h≈H`; confirms no half-cell offset crept into the common case.

### Phase 4 — finish
- [ ] Add a short "Building images" note to the README beside "Connection sprites".
- [ ] Optional: ModOptions toggle + resolution field.
- [ ] Confirm no icon trips the website's 5 MB/icon test at 200 px.
- [ ] Delete this plan file.

## Risks / watch-items

- Some buildings render parts beyond their footprint; tight-crop captures them faithfully (that's
  the real anim), but spot-check against the website's footprint-stretch for distortion.
- Off-screen spawning must not perturb a live colony — spawn far from the camera and destroy
  immediately, exactly as the connection tool does.
- Full-export run order: main-menu pass first (all icons), then the in-game building pass
  (overwrites building icons). Document this.
