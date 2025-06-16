using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Path = System.IO.Path;

namespace OniExtract2024
{
    public class BaseExport
    {
        public virtual string DatabaseDirName { get; set; } = "database";
        public virtual string ExportFileName { get; set; } = "database";
        public string buildVersion = BuildWatermark.GetBuildText();
        public List<string> dlcs = new List<string>(); 

        public BaseExport()
        {
            foreach (string dlc in DlcManager.GetActiveDLCIds())
            {
                dlcs.Add(dlc);
            }
            if(!dlcs.Contains(DlcManager.VANILLA_ID))
            {
                dlcs.Add(DlcManager.VANILLA_ID);
            }
        }

        public string GetDatabaseLocation()
        {
            string exportDir = DatabaseDirName;
            if (!DlcManager.IsExpansion1Active())
            {
                exportDir += "_base";
            }
            return Path.Combine(Util.RootFolder(), "export", exportDir);
        }

        public void ExportJsonFile()
        {
            if (!Directory.Exists(GetDatabaseLocation()))
            {
                Directory.CreateDirectory(GetDatabaseLocation());
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            File.WriteAllText(Path.Combine(GetDatabaseLocation(), ExportFileName + ".json"), JsonConvert.SerializeObject(this, settings));
        }
    }
}