using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Business.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Insert(T s);
        Task<T> Update(T s);
        Task<List<T>> GetList();
        Task<T> FindById(int id);
        Task<bool> Delete(T s);
    }
}
