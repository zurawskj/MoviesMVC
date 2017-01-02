using System;

namespace Movies.Services.DomainModels
{
    public class MovieDomainModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
    }
}
