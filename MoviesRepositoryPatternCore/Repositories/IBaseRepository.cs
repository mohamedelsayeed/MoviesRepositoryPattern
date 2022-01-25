using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryPattern.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetGenreById(int id);
        Task<IEnumerable<T>> GetAllGenre();
        Task<T> CreateGenre(T entity);
        Task<IEnumerable<T>> CreateListGenre(IEnumerable<T> entity);
        Task<int> Complete();
        T UpdateGenre(T entity);
        void DeleteGenre(T entity);

    }
}
