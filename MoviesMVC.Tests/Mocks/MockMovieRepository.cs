using Movies.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesMVC.Tests.Mocks
{
    class MockMovieRepository : IMovieRepository
    {
        List<Movie> _movies;

        public MockMovieRepository()
        {
            _movies = new List<Movie>();
        }

        public async Task<int> Add(Movie movie)
        {
            return await Task.Run<int>(() =>
            {
                _movies.Add(movie);
                return 0;
            });
        }

        public async Task<int> Delete(Movie movie)
        {
            return await Task.Run(() =>
            {
                _movies.Remove(movie);
                return 0;
            });
        }

        public async Task<int> Edit(Movie movie)
        {
            return await Task.Run(() =>
            {
                var movieToEdit = _movies.First(m => m.ID == movie.ID);
                _movies.Remove(movieToEdit);
                _movies.Add(movie);
                return 0;
            });
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await Task.Run(() => { return _movies; });
        }

        public async Task<Movie> GetByIdAsync(int? id)
        {
            return await Task.Run(() => { return _movies.First(m => m.ID == (int)id); });
        }
    }
}
