using Models.DTOs;
using Models.RequestModels;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IServiceDocumentDetails : IService<DocumentDetailsDTO>
    {
        Task<DocumentDetailsDTO> Insert(DocumentDetailsRequestModel model);
        Task<DocumentDetailsDTO> Update(DocumentDetailsRequestModel model, int id);
    }
}
