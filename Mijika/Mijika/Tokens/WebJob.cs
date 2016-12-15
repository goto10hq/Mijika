using Newtonsoft.Json;

namespace Mijika.Tokens
{
    public class WebJob
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("latest_run")]
        public Run LatestRun { get; set; }
    }
}
