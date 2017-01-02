using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MoviesMVC.ViewModels
{
    public class CreateMovieViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public List<string> Genres { get; set; }
    }
}