using Newtonsoft.Json;
using System;

namespace TvMaze.Client.Models
{
    public class LinkDefinition
    {
        [JsonProperty("href")]
        public Uri Uri { get; set; }
    }
}
