using Newtonsoft.Json;

namespace TvMaze.Client.Models
{
    public class Rating
    {
        [JsonProperty("average")]
        public double? Average { get; set; }
    }
}
