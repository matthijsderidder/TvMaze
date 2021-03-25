using Newtonsoft.Json;

namespace TvMaze.Client.Models
{
    public class ShowLinks
    {
        [JsonProperty("self")]
        public LinkDefinition Self { get; set; }

        [JsonProperty("previousepisode", NullValueHandling = NullValueHandling.Ignore)]
        public LinkDefinition PreviousEpisode { get; set; }
    }
}
