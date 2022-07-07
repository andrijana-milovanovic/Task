using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> FindById(int id);
        Task<bool> Delete(int id);
    }
}
