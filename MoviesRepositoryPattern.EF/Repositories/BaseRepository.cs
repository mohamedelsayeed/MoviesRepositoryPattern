using Microsoft.EntityFrameworkCore;
using MoviesRepositoryPattern.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryPattern.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateGenre(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllGenre()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetGenreById(byte id)
        {

            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public T UpdateGenre(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void DeleteGenre(T entity)
        {
           _context.Set<T>().Remove(entity);
        }
    }
}
