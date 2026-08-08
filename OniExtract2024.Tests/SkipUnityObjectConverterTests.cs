using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace OniExtract2024.Tests
{
    public class SkipUnityObjectConverterTests
    {
        private readonly SkipUnityObjectConverter _converter = new SkipUnityObjectConverter();

        [Fact]
        public void CanConvert_UnityObject_ReturnsTrue()
        {
            Assert.True(_converter.CanConvert(typeof(UnityEngine.Object)));
        }

        [Fact]
        public void CanConvert_UnityObjectSubclass_ReturnsTrue()
        {
            Assert.True(_converter.CanConvert(typeof(UnityEngine.Transform)));
        }

        [Fact]
        public void CanConvert_PlainClass_ReturnsFalse()
        {
            Assert.False(_converter.CanConvert(typeof(string)));
        }

        [Fact]
        public void WriteJson_WritesNull()
        {
            var sw = new StringWriter();
            using (var writer = new JsonTextWriter(sw))
            {
                _converter.WriteJson(writer, null, JsonSerializer.CreateDefault());
            }
            Assert.Equal("null", sw.ToString());
        }
    }
}
