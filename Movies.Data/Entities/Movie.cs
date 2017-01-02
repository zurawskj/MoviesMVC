using System;

namespace Movies.Data.Models
{

    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}