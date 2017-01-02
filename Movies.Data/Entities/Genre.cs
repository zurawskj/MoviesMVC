using System.Collections.Generic;

namespace Movies.Data.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
