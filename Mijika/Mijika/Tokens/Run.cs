using System;
using Newtonsoft.Json;

namespace Mijika.Tokens
{
    public class Run
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        [JsonProperty("output_url")]
        public string OutputUrl { get; set; }

        [JsonProperty("error_url")]
        public string ErrorUrl { get; set; }
    }
}
