using Movies.Services.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Movies.Services.Enums.Enums;

namespace Movies.Services
{
    public interface IMovieService
    {
        Task<List<MovieDomainModel>> GetAllAsync();

        Task<MovieDomainModel> GetByIdAsync(int? id);

        Task<int> Add(MovieDomainModel movie);

        Task<int> Edit(MovieDomainModel movie);

        Task<int> Delete(MovieDomainModel movie);

        Task<List<string>> GetAllGenres();

        Task<TicketOrderStatus> OrderTicket(string customer);
    }
}