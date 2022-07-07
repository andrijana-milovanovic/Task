using Models.DTOs;
using Models.RequestModels;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IServiceDocument : IService<DocumentDTO>
    {
        Task<DocumentDTO> Insert(DocumentRequestModel model);
        Task<DocumentDTO> Update(DocumentRequestModel model, int id);
        Task<DocumentDTO> InsertDocumentWithDetails(DocumentWithDetailsRequestModel model);
    }
}
