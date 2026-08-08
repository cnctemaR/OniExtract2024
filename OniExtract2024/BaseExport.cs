using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Path = System.IO.Path;

namespace OniExtract2024
{
    // Unity 6 changed Transform.childCount to use TransformHandle internally.
    // When JSON.NET tries to serialize a UnityEngine.Object (e.g. Transform is IEnumerable<Transform>),
    // it crashes accessing a null/destroyed TransformHandle. These two classes prevent that.
    class SkipUnityObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            typeof(UnityEngine.Object).IsAssignableFrom(objectType);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteNull();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            null;
    }

    class SkipUnityObjectContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (prop.PropertyType != null && typeof(UnityEngine.Object).IsAssignableFrom(prop.PropertyType))
                prop.ShouldSerialize = _ => false;
            return prop;
        }
    }

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

        public static string BuildExportPath(string rootFolder, string dirName, bool isExpansion1Active)
        {
            string suffix = isExpansion1Active ? "" : "_base";
            return Path.Combine(rootFolder, "export", dirName + suffix);
        }

        public string GetDatabaseLocation() =>
            BuildExportPath(Util.RootFolder(), DatabaseDirName, DlcManager.IsExpansion1Active());

        public void ExportJsonFile()
        {
            if (!Directory.Exists(GetDatabaseLocation()))
            {
                Directory.CreateDirectory(GetDatabaseLocation());
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new SkipUnityObjectContractResolver();
            settings.Converters.Add(new SkipUnityObjectConverter());
            File.WriteAllText(Path.Combine(GetDatabaseLocation(), ExportFileName + ".json"), JsonConvert.SerializeObject(this, settings));
        }
    }
}
