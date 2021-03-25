using Newtonsoft.Json;
using System;

namespace TvMaze.Client.Models
{
    public class Image
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }
}
