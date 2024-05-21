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
        public string[] dlcs = GetDlcs();

        public string GetDatabaseLocation()
        {
            return Path.Combine(Util.RootFolder(), "export", DatabaseDirName);
        }

        public static string[] GetDlcs()
        {
            bool base_only = DlcManager.IsDlcListValidForCurrentContent(DlcManager.AVAILABLE_VANILLA_ONLY);
            bool dlc1_only = DlcManager.IsDlcListValidForCurrentContent(DlcManager.AVAILABLE_EXPANSION1_ONLY);
            bool all_available = DlcManager.IsDlcListValidForCurrentContent(DlcManager.AVAILABLE_ALL_VERSIONS);
            if (all_available)
            {
                if (base_only) return DlcManager.AVAILABLE_VANILLA_ONLY;
                else return DlcManager.AVAILABLE_ALL_VERSIONS;
            }
            else if (base_only)
            {
                return DlcManager.AVAILABLE_VANILLA_ONLY;
            }
            else if (dlc1_only)
            {
                return DlcManager.AVAILABLE_EXPANSION1_ONLY;
            }
            return null;
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