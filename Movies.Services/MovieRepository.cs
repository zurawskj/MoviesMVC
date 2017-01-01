using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Movies.Services
{

    public class MovieRepository : IMovieRepository, IDisposable
    {
        public MovieDbContext _movieDbContext;

        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _movieDbContext.Movies.ToListAsync(); 
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies.FindAsync(id);
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _movieDbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int? id)
        {
            return await _movieDbContext.Movies.FindAsync(id);
        }

        public async Task<int> Add(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
            return await _movieDbContext.SaveChangesAsync();
        }

        public async Task<int> Edit(Movie movie)
        {
            _movieDbContext.Entry(movie).State = EntityState.Modified;
            return await _movieDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Movie movie)
        {
            _movieDbContext.Movies.Remove(movie);
            return await _movieDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _movieDbContext.Dispose();
        }
    }
}
