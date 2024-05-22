using OniExtract2024;
using System.Collections.Generic;
using static GeoTunerConfig;

public class ExportGeyser : BaseExport
{
    public override string ExportFileName { get; set; } = "geyser";
    public List<GeyserGenericConfig.GeyserPrefabParams> geysers;
    public Dictionary<GeoTunerConfig.Category, GeoTunerConfig.GeotunedGeyserSettings> CategorySettings = GeoTunerConfig.CategorySettings;
    public Dictionary<HashedString, GeotunedGeyserSettings> geotunerGeyserSettings = GeoTunerConfig.geotunerGeyserSettings;
    public Dictionary<string, string> geyserIdHashDictionary = new Dictionary<string, string>();

    public ExportGeyser()
	{
        geysers = new List<GeyserGenericConfig.GeyserPrefabParams>();
    }

    public void AddGeyserPrefabParams(List<GeyserGenericConfig.GeyserPrefabParams> geysersParams)
    {
        geysersParams.ForEach(param => {
            this.geysers.Add(param);
            this.geyserIdHashDictionary.Add(param.geyserType.id, new HashedString(param.geyserType.id).ToString());
        });
    }
}
