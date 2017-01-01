using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();

        Task<Movie> GetByIdAsync(int? id);

        Task<int> Add(Movie movie);

        Task<int> Edit(Movie movie);

        Task<int> Delete(Movie movie);
    }
}