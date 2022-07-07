using Models.DTOs;
using Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IServiceCustomer : IService<CustomerDTO>
    {
        Task<CustomerDTO> Insert(CustomerRequestModel model);
        Task<CustomerDTO> Update(CustomerRequestModel model, int id);

        Task<DocumentViewModel> GetDocumentForCustomer(int customerId, int documentId);
        Task<List<DocumentViewModel>> GetAllDocumentsForCustomer(int id);
    }
}
