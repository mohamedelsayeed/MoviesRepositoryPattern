using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoviesRepositoryPattern.Core.Consts;
namespace MoviesRepositoryPattern.Core.Repositories
{
    public interface IMoivesRepository<T> where T : class
    {
        Task<T> GetMovieById(byte id);
        Task<IEnumerable<T>> GetAllMovies(string []includes = null);
        Task<T> CreateMovie(T entity);
        Task<int> Complete();
        T UpdateMovie(T entity);
        void DeleteMovie(T entity);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
          Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

    }
}
