using Movies.Services;
using MoviesMVC.Models;

namespace MoviesMVC
{
    public static class ExtensionMethods
    {
        public static MovieViewModel ToViewModel(this Movie movie)
        {
            return new MovieViewModel
            {
                ID = movie.ID,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            };
        }
    }
}