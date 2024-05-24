using System;
using HarmonyLib;
using PeterHan.PLib.Options;

namespace OniExtract2024 {
  public class Mod : KMod.UserMod2{
    public override void OnLoad(Harmony harmony) {
      base.OnLoad(harmony);
      new POptions().RegisterOptions(this, typeof(ModOptions));
    }
  }
}