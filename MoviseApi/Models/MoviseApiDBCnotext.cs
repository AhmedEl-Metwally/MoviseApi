using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MoviseApi.Models
{
    public class MoviseApiDBCnotext :DbContext
    {
        public MoviseApiDBCnotext(DbContextOptions<MoviseApiDBCnotext> options) : base(options)
        {
        }
        public DbSet<Genre>Genres { get; set; }
        public DbSet<Movie>Movies { get; set; }


    }
}
