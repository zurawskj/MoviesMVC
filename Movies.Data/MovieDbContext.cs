using Movies.Data.Models;
using System.Data.Entity;

namespace Movies.Data
{

    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
            :base("name=MovieDbContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}