using System.ComponentModel.DataAnnotations;
using TvMaze.Data.Models;

namespace TvMaze.Data.Relations
{
    public class ShowPerson
    {
        [Key]
        public int ShowId { get; set; }

        [Key]
        public int PersonId { get; set; }

        public virtual Show Show { get; set; }

        public virtual Person Person { get; set; }
    }
}
