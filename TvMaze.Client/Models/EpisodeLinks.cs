using Newtonsoft.Json;

namespace TvMaze.Client.Models
{
    public class EpisodeLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }
    }
}
