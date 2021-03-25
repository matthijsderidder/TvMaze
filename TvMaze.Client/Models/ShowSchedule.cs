using Newtonsoft.Json;
using System.Collections.Generic;

namespace TvMaze.Client.Models
{
    public class ShowSchedule
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }
    }
}
