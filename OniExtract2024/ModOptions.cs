using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace OniExtract2024
{
    [ConfigFile("OniExtract2024.json", true, true)]
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public class ModOptions : SingletonOptions<ModOptions>
    {
        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Building", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Building { get; set; } = true;
        
        [JsonProperty]
        [Option("STRINGS.Options.JsonData.db", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool db { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Entities", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Entities { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.MultiEntities", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool MultiEntities { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Element", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Element { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Item", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Item { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Food", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Food { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Geyser", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Geyser { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.PoString", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool PoString { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Recipe", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Recipe { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.Tags", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool Tags { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.Options.JsonData.UISprintInfo", null, "STRINGS.Options.JsonData.CATEGORY_NAME")]
        public bool UISprintInfo { get; set; } = true;

        [JsonProperty] 
        [Option("STRINGS.Options.UIFileName.NAME","STRINGS.Options.UIFileName.TOOLTIP")] 
        public SaveNameMod SaveUIFileName { get; set; } = SaveNameMod.ID;
        
        
        public enum SaveNameMod
        {
            [Option("STRINGS.Options.UIFileName.Tag")]
            ID,
            [Option("STRINGS.Options.UIFileName.ProperName")]
            Proper
        }
        public ModOptions()
        {
        }
    }
    
}
