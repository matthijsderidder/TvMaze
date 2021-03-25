using Newtonsoft.Json;

namespace TvMaze.Client.Models
{
    public class Externals
    {
        [JsonProperty("tvrage")]
        public long? TVRage { get; set; }

        [JsonProperty("thetvdb")]
        public long? TheTVDb { get; set; }

        [JsonProperty("imdb")]
        public string Imdb { get; set; }
    }
}
