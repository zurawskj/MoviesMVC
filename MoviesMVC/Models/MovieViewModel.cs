using Movies.Services;
using System;

namespace MoviesMVC.Models
{
    public class MovieViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public Movie ToMovie()
        {
            return new Movie
            {
                ID = ID,
                Title = Title,
                ReleaseDate = ReleaseDate,
                Genre = Genre,
                Price = Price
            };
        }
    }
}