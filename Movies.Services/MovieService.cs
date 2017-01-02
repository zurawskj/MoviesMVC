using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Data;
using System.Data.Entity;
using System.Linq;
using Movies.Services.DomainModels;
using Movies.Data.Models;

namespace Movies.Services
{

    public class MovieService : IMovieService, IDisposable
    {
        public MovieDbContext _movieDbContext;

        public MovieService(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<MovieDomainModel> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies
                            .Join(
                                _movieDbContext.Genres,
                                m => m.GenreId,
                                g => g.ID,
                                (m, g) => new { m, g }
                            )
                            .Select(mg => new MovieDomainModel
                                {
                                    ID = mg.m.ID,
                                    Title = mg.m.Title,
                                    ReleaseDate = mg.m.ReleaseDate,
                                    Price = mg.m.Price,
                                    Genre = mg.g.Name
                                }
                            ).FirstOrDefaultAsync();
        }

        public async Task<List<MovieDomainModel>> GetAllAsync()
        {
            return await _movieDbContext.Movies
                            .Join(
                                _movieDbContext.Genres,
                                m => m.GenreId,
                                g => g.ID,
                                (m, g) => new { m, g }
                            )
                            .Select(mg => new MovieDomainModel
                            {
                                ID = mg.m.ID,
                                Title = mg.m.Title,
                                ReleaseDate = mg.m.ReleaseDate,
                                Price = mg.m.Price,
                                Genre = mg.g.Name
                            }).ToListAsync();
        }

        public async Task<MovieDomainModel> GetByIdAsync(int? id)
        {
            return await _movieDbContext.Movies
                            .Where(m => m.ID == id)
                            .Join(
                                _movieDbContext.Genres,
                                m => m.GenreId,
                                g => g.ID,
                                (m, g) => new { m, g }
                            )
                            .Select(mg => new MovieDomainModel
                            {
                                ID = mg.m.ID,
                                Title = mg.m.Title,
                                ReleaseDate = mg.m.ReleaseDate,
                                Price = mg.m.Price,
                                Genre = mg.g.Name
                            })
                            .FirstOrDefaultAsync();
        }

        public async Task<int> Add(MovieDomainModel movie)
        {
            //Check for existing genre
            Genre genre = await _movieDbContext.Genres.Where(g => g.Name.ToLower() == movie.Genre.ToLower()).FirstOrDefaultAsync();
            if (genre == null)
                return -1;

            using (var transaction = _movieDbContext.Database.BeginTransaction())
            {
                try
                {
                    Movie movieToAdd = new Movie
                    {
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        Price = movie.Price,
                        GenreId = genre.ID
                    };

                    _movieDbContext.Movies.Add(movieToAdd);

                    await _movieDbContext.SaveChangesAsync();

                    transaction.Commit();

                    return 0;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public async Task<int> Edit(MovieDomainModel movie)
        {
            //Check for existing genre
            Genre genre = await _movieDbContext.Genres.Where(g => g.Name.ToLower() == movie.Genre.ToLower()).FirstOrDefaultAsync();
            if (genre == null)
                return -1;

            using (var transaction = _movieDbContext.Database.BeginTransaction())
            {
                try
                {
                   Movie movieToEdit = _movieDbContext.Movies.Where(m => m.ID == movie.ID).FirstOrDefault();
                    if (movieToEdit == null)
                        return -1;

                    movieToEdit.Title = movie.Title;
                    movieToEdit.ReleaseDate = movie.ReleaseDate;
                    movieToEdit.Price = movie.Price;
                    movieToEdit.GenreId = genre.ID;

                    _movieDbContext.Entry(movieToEdit).State = EntityState.Modified;                    

                    await _movieDbContext.SaveChangesAsync();

                    transaction.Commit();

                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public async Task<int> Delete(MovieDomainModel movie)
        {
            Movie movieToDelete = _movieDbContext.Movies.Where(m => m.ID == movie.ID).FirstOrDefault();
            if (movieToDelete == null)
                return -1;

            _movieDbContext.Movies.Remove(movieToDelete);
            return await _movieDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _movieDbContext.Dispose();
        }

        public async Task<List<string>> GetAllGenres()
        {
            return await _movieDbContext.Genres.Select(g => g.Name).ToListAsync();
        }

        public Task<Enums.Enums.TicketOrderStatus> OrderTicket(string customer)
        {
            throw new NotImplementedException();
        }
    }
}
