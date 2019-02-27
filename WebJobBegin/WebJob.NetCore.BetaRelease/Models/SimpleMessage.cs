using Newtonsoft.Json;

namespace WebJob.NetCore.BetaRelease.Models
{
    public class SimpleMessage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
