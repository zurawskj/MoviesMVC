using System.Data.Entity;

namespace Movies.Services
{

    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}