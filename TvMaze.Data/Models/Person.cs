using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TvMaze.Data.Converters;
using TvMaze.Data.Relations;

namespace TvMaze.Data.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? Birthday { get; set; }

        [JsonIgnore]
        public ICollection<Show> Shows { get; set; } = new List<Show>();

        [JsonIgnore]
        public IList<ShowPerson> ShowPersons { get; set; }
    }
}
