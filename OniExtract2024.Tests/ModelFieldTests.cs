using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace OniExtract2024.Tests
{
    public class ModelFieldTests
    {
        [Fact]
        public void BVector2_SerializesToXY()
        {
            var v = new BVector2(1.5f, 2.5f);
            var j = JObject.Parse(JsonConvert.SerializeObject(v));
            Assert.Equal(1.5f, j["x"].Value<float>());
            Assert.Equal(2.5f, j["y"].Value<float>());
        }

        [Fact]
        public void BColor_SerializesToRGBA()
        {
            var c = new BColor(0.25f, 0.5f, 0.75f, 1.0f);
            var j = JObject.Parse(JsonConvert.SerializeObject(c));
            Assert.Equal(0.25f, j["r"].Value<float>());
            Assert.Equal(0.5f, j["g"].Value<float>());
            Assert.Equal(0.75f, j["b"].Value<float>());
            Assert.Equal(1.0f, j["a"].Value<float>());
        }
    }
}
