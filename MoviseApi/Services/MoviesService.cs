using Microsoft.EntityFrameworkCore;
//using MoviseApi.Models;


namespace MoviseApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviseApiDBCnotext _context;

        public MoviesService(MoviseApiDBCnotext cnotext)
        {
            this._context = cnotext;
        }


        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;   
        }


        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        }


        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId || genreId ==0)
                .OrderByDescending(m => m.Ratr)
                .Include(m => m.Genre)
                .ToListAsync();

        }


        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }


        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}
