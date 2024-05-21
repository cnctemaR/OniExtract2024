using Newtonsoft.Json;
using System.IO;
using Path = System.IO.Path;

namespace OniExtract2024
{
    public class BaseExport
    {
        public virtual string DatabaseDirName { get; set; } = "database";
        public virtual string ExportFileName { get; set; } = "database";
        public string buildVersion = BuildWatermark.GetBuildText();

        public string GetDatabaseLocation()
        {
            return Path.Combine(Util.RootFolder(), "export", DatabaseDirName);
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