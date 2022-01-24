using Microsoft.EntityFrameworkCore;
using MoviesRepositoryPattern.Core.Consts;
using MoviesRepositoryPattern.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryPattern.EF.Repositories
{
    public class MoviesRepository<T> : IMoivesRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateMovie(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllMovies(string[] includes = null)
        {
            if(includes != null)
                foreach(var include in includes)
                return await _context.Set<T>().Include(include).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetMovieById(byte id)
        {

            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public T UpdateMovie(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void DeleteMovie(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

    }
}
