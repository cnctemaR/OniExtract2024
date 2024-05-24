using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace OniExtract2024 {
  [ConfigFile("OniExtract2024.json", true, true)]
  [JsonObject(MemberSerialization.OptIn)]
  [RestartRequired]
  public class ModOptions : SingletonOptions<ModOptions> {
    [JsonProperty]
    [Option("STRINGS.OPTION.UI_NAME_PATTERN.NAME", "STRINGS.OPTION.UI_NAME_PATTERN.TOOLTIP", null)] 
    public string UINamePattern { get; set; }
    
    public ModOptions() {
      UINamePattern = "{tag}";
    }
  }
  
  public static class StringKeys {
    public const string UINamePattern_Option_NAME = "STRINGS.OPTION.UI_NAME_PATTERN.NAME";
    public const string UINamePattern_Option_TOOLTIP = "STRINGS.OPTION.UI_NAME_PATTERN.TOOLTIP";
  }
}