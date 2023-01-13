using Microsoft.EntityFrameworkCore;
using MoviseApi.Models;

namespace MoviseApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly MoviseApiDBCnotext _context;

        public GenresService(MoviseApiDBCnotext cnotext)
        {
            this._context = cnotext;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _context .Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;  
        }

        public async Task<Genre> GetById(byte id)
        {
            var genres = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            return genres;
        }

        public Genre Update(Genre genre)

        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<bool> IsvalidGenre(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
