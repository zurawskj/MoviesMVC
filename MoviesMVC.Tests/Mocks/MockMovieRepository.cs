using Movies.Data.Models;
using Movies.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Services.DomainModels;
using Movies.Services.Enums;
using System;

namespace MoviesMVC.Tests.Mocks
{
    class MockMovieRepository : IMovieService
    {
        List<Movie> _movies;
        List<Genre> _genres;

        public MockMovieRepository()
        {
            _movies = new List<Movie>();
            _genres = new List<Genre>();
        }

        public Task<int> Add(MovieDomainModel movie)
        {
            try
            {
                Genre genre = _genres.Where(g => g.Name.ToLower() == movie.Genre.ToLower()).FirstOrDefault();
                if (genre == null)
                {
                    _genres.Add(new Genre { ID = _genres.Count, Name = movie.Genre });
                    genre = _genres.Where(g => g.ID == _genres.Count - 1).FirstOrDefault();
                }
                _movies.Add(new Movie
                {
                    ID = _movies.Count,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Price = movie.Price,
                    GenreId = genre.ID
                });

                return Task.FromResult(0);
            }
            catch(Exception ex)
            {
                return Task.FromResult(-1);
            }
        }

        public Task<int> Delete(MovieDomainModel movie)
        {
            try
            {
                Genre genre = _genres.Where(g => g.Name.ToLower() == movie.Genre.ToLower()).FirstOrDefault();
                _movies.Remove(_movies.Where(m => m.ID == movie.ID).FirstOrDefault());

                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(-1);
            }
        }

        public Task<int> Edit(MovieDomainModel movie)
        {
            try
            {
                Genre genre = _genres.Where(g => g.Name.ToLower() == movie.Genre.ToLower()).FirstOrDefault();
                if (genre == null)
                {
                    _genres.Add(new Genre { ID = _genres.Count, Name = movie.Genre });
                    genre = _genres.Where(g => g.ID == _genres.Count - 1).FirstOrDefault();
                }
                var index = _movies.FindIndex(m => m.ID == movie.ID);
                _movies[index].Title = movie.Title;
                _movies[index].ReleaseDate = movie.ReleaseDate;
                _movies[index].Price = movie.Price;
                _movies[index].GenreId = genre.ID;


                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(-1);
            }
        }

        public Task<List<string>> GetAllGenres()
        {
            return Task.FromResult(_genres.Select(g => g.Name).ToList());
        }

        public Task<Enums.TicketOrderStatus> OrderTicket(string customer)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieDomainModel>> GetAllAsync()
        {
            return Task.FromResult(
                _movies.Join<Movie, Genre, int, dynamic>(
                        _genres,
                        m => m.GenreId,
                        g => g.ID,
                        (m, g) => new { m, g } )
                    .Select(
                        mg => new MovieDomainModel
                        {
                            ID = mg.m.ID,
                            ReleaseDate = mg.m.ReleaseDate,
                            Price = mg.m.Price,
                            Genre = mg.g.Name
                        }
                    ).ToList()
            );
        }

        public Task<MovieDomainModel> GetByIdAsync(int? id)
        {
            if (id == null)
                return null;

            return Task.FromResult(
                _movies
                .Where(m => m.ID == id)
                .Join<Movie, Genre, int, dynamic>(
                        _genres,
                        m => m.GenreId,
                        g => g.ID,
                        (m, g) => new { m, g })
                    .Select(
                        mg => new MovieDomainModel
                        {
                            ID = mg.m.ID,
                            Title = mg.m.Title,
                            ReleaseDate = mg.m.ReleaseDate,
                            Price = mg.m.Price,
                            Genre = mg.g.Name
                        }
                    ).FirstOrDefault()
            );
        }
    }
}
