using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mijika.Tokens
{
    public class History
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }
}
