using Models.Entities;
using System.Threading.Tasks;

namespace Data.Business.Repositories
{
    public interface IRepositoryCustomer : IRepository<Customer>
    {
        Task<Customer> GetCustomerWithDocument(int customerId, int documentId);
        Task<Customer> GetCustomerWithDocuments(int id);
    }
}
