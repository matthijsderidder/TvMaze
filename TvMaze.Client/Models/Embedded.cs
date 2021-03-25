using Newtonsoft.Json;
using System.Collections.Generic;

namespace TvMaze.Client.Models
{
    public class Embedded
    {
        [JsonProperty("episodes")]
        public IEnumerable<Episode> Episodes { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }

        [JsonProperty("character")]
        public Show Character { get; set; }
    }
}
