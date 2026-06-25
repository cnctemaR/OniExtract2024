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
            // ONI builds after 623230: serializing live Unity/game component graphs can hit
            // property getters that throw on prefab templates (e.g. a null TransformHandle),
            // which aborted the whole dump. Skip any member that throws and keep going so the
            // serializable data (rates, recipes, elements, etc.) still gets written.
            settings.Error = (sender, args) =>
            {
                args.ErrorContext.Handled = true;
            };
            // Do NOT recurse into live UnityEngine.Object graphs (Material/GameObject/Component/
            // KAnimFile/Transform...). On ONI builds after 623230 those getters can throw
            // (null TransformHandle) and the graph is huge/cyclic enough to hang the dump.
            // Emit just the object name. All data we need is already copied into plain fields.
            settings.Converters.Add(new UnityObjectJsonConverter());
            // Serialize Element as scalar fields only — skips the substance/material/anim graph
            // that StackOverflows. Keeps the physical properties we actually want.
            settings.Converters.Add(new ElementJsonConverter());
            File.WriteAllText(Path.Combine(GetDatabaseLocation(), ExportFileName + ".json"), JsonConvert.SerializeObject(this, settings));
        }
    }

    // Serializes any UnityEngine.Object as just its name (or null) and never recurses into it.
    // Prevents both the null-TransformHandle NullReferenceException and the deep/cyclic graph
    // traversal that hung the Elements/Building/Entity dumps on ONI builds after 623230.
    public class UnityObjectJsonConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return typeof(UnityEngine.Object).IsAssignableFrom(objectType);
        }

        public override bool CanRead { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            UnityEngine.Object uo = value as UnityEngine.Object;
            // Unity overloads == so this also catches destroyed / "fake null" objects.
            if (object.ReferenceEquals(uo, null) || uo == null)
            {
                writer.WriteNull();
                return;
            }
            string n = null;
            try { n = uo.name; } catch { }
            writer.WriteValue(n);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }
    }

    // Serializes an Element as ONLY its scalar members (primitives, enums, strings, Tags),
    // skipping reference-typed members like `substance` whose deep material/anim graph
    // stack-overflows the serializer. Reflection-based so it survives game field renames.
    public class ElementJsonConverter : JsonConverter
    {
        public override bool CanConvert(System.Type t) { return t == typeof(Element); }
        public override bool CanRead { get { return false; } }

        public override void WriteJson(JsonWriter w, object value, JsonSerializer s)
        {
            if (value == null) { w.WriteNull(); return; }
            var t = value.GetType();
            var flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
            var seen = new System.Collections.Generic.HashSet<string>();
            w.WriteStartObject();
            foreach (var f in t.GetFields(flags))
                WriteMember(w, seen, f.Name, SafeGet(delegate { return f.GetValue(value); }), f.FieldType);
            foreach (var p in t.GetProperties(flags))
            {
                if (!p.CanRead || p.GetIndexParameters().Length > 0) continue;
                var prop = p;
                WriteMember(w, seen, prop.Name, SafeGet(delegate { return prop.GetValue(value, null); }), prop.PropertyType);
            }
            w.WriteEndObject();
        }

        static object SafeGet(System.Func<object> g) { try { return g(); } catch { return null; } }

        static void WriteMember(JsonWriter w, System.Collections.Generic.HashSet<string> seen, string name, object v, System.Type ft)
        {
            if (!seen.Add(name)) return;
            if (ft == typeof(Tag)) { w.WritePropertyName(name); w.WriteValue(v == null ? null : ((Tag)v).Name); return; }
            if (ft.IsEnum) { w.WritePropertyName(name); w.WriteValue(v == null ? null : v.ToString()); return; }
            if (ft.IsPrimitive || ft == typeof(string) || ft == typeof(decimal))
            {
                w.WritePropertyName(name);
                w.WriteValue(v);
                return;
            }
            seen.Remove(name); // not written — allow a later field/prop of same name to try
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }
    }
}