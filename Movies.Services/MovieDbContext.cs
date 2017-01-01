using System.Data.Entity;

namespace Movies.Services
{

    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
            :base("name=MovieDbContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}