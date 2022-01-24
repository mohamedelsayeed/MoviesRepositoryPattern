using Microsoft.EntityFrameworkCore;
using MoviesRepositoryPattern.Core.Models;

namespace MoviesRepositoryPattern.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
