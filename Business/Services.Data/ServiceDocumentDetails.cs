using Data.Business.Repositories;
using Models.DTOs;
using Models.Entities;
using Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Data
{
    public class ServiceDocumentDetails : IServiceDocumentDetails
    {
        private readonly IRepositoryDocumentDetails _repositoryDocumentDetails;

        private readonly IRepositoryDocument _repositoryDocument;

        public ServiceDocumentDetails(IRepositoryDocumentDetails repositoryDocumentDetails, IRepositoryDocument repositoryDocument)
        {
            _repositoryDocumentDetails = repositoryDocumentDetails;
            _repositoryDocument = repositoryDocument;
        }

        public async Task<DocumentDetailsDTO> FindById(int id)
        {
            var documentDetails = await _repositoryDocumentDetails.FindById(id);

            var documentDetailsDTO = new DocumentDetailsDTO
            {
                Id = id,
                DocumentId = documentDetails.DocumentId,
                ArticleName = documentDetails.ArticleName,
                Quantity = documentDetails.Quantity,
                Price = documentDetails.Price
            };

            return documentDetailsDTO;
        }

        public async Task<List<DocumentDetailsDTO>> GetList()
        {
            var documentsDetails = await _repositoryDocumentDetails.GetList();

            if(documentsDetails is null)
            {
                return null;
            }

            List<DocumentDetailsDTO> documentDetailsDTOs = DocumentDetailsEntityToDTO(documentsDetails);

            return documentDetailsDTOs;
        }

        private static List<DocumentDetailsDTO> DocumentDetailsEntityToDTO(List<DocumentDetails> documentsDetails)
        {
            List<DocumentDetailsDTO> documentDetailsDTOs = new List<DocumentDetailsDTO>();

            documentsDetails.ForEach(documentDetails =>
            {
                documentDetailsDTOs.Add(new DocumentDetailsDTO
                {
                    Id = documentDetails.Id,
                    DocumentId = documentDetails.DocumentId,
                    ArticleName = documentDetails.ArticleName,
                    Quantity = documentDetails.Quantity,
                    Price = documentDetails.Price
                });
            });

            return documentDetailsDTOs;
        }

        public async Task<bool> Delete(int id)
        {
            var documetnDetails = await _repositoryDocumentDetails.FindById(id);

            if (documetnDetails == null)
            {
                return false;
            }

            return await _repositoryDocumentDetails.Delete(documetnDetails);
        }

        public async Task<DocumentDetailsDTO> Insert(DocumentDetailsRequestModel model)
        {
            var document = await _repositoryDocument.FindById(model.DocumentId);

            var documentDetails = new DocumentDetails
            {
                Document = document,
                ArticleName = model.ArticleName,
                Price = model.Price,
                Quantity = model.Quantity
            };

            var result = await _repositoryDocumentDetails.Insert(documentDetails);

            var documentDetailsDTO = new DocumentDetailsDTO
            {
                Id = result.Id,
                DocumentId = result.DocumentId,
                ArticleName = result.ArticleName,
                Price = result.Price,
                Quantity = result.Quantity
            };

            return documentDetailsDTO;
        }

        public async Task<DocumentDetailsDTO> Update(DocumentDetailsRequestModel model, int id)
        {
            var documentDetails = await _repositoryDocumentDetails.FindById(id);
            
            documentDetails.ArticleName = model.ArticleName;
            documentDetails.Price = model.Price;
            documentDetails.Quantity = model.Quantity;

            var result = await _repositoryDocumentDetails.Update(documentDetails);

            var documentDetailsDTO = new DocumentDetailsDTO
            {
                Id = result.Id,
                DocumentId = result.DocumentId,
                ArticleName = result.ArticleName,
                Price = result.Price,
                Quantity = result.Quantity
            };

            return documentDetailsDTO;
        }
    }
}
