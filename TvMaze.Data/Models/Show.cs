using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TvMaze.Data.Relations;

namespace TvMaze.Data.Models
{
    public class Show
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> Cast { get; set; } = new List<Person>();

        [JsonIgnore]
        public IList<ShowPerson> ShowPersons { get; set; }
    }
}
