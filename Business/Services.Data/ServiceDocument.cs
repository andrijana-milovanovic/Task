using Data.Business.Repositories;
using Models.DTOs;
using Models.Entities;
using Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Data
{
    public class ServiceDocument : IServiceDocument
    {
        private readonly IRepositoryDocument _repositoryDocument;
        private readonly IRepositoryCustomer _repositoryCustomer;

        public ServiceDocument(IRepositoryDocument repositoryDocument, IRepositoryCustomer repositoryCustomer)
        {
            _repositoryDocument = repositoryDocument;
            _repositoryCustomer = repositoryCustomer;
        }

        public async Task<bool> Delete(int id)
        {
            var document = await _repositoryDocument.FindById(id);

            if (document == null)
            {
                return false;
            }

            return await _repositoryDocument.Delete(document);
        }

        public async Task<DocumentDTO> FindById(int id)
        {
            var document = await _repositoryDocument.FindById(id);

            var documentDTO = new DocumentDTO
            {
                Id = id,
                Date = document.Date,
                Note = document.Note
            };

            return documentDTO;
        }

        public async Task<List<DocumentDTO>> GetList()
        {
            var documents = await _repositoryDocument.GetList();

            if(documents is null)
            {
                return null;
            }

            List<DocumentDTO> documentDTOs = DocumentEntityToDTO(documents);

            return documentDTOs;
        }

        private static List<DocumentDTO> DocumentEntityToDTO(List<Document> documents)
        {
            List<DocumentDTO> documentDTOs = new List<DocumentDTO>();
            documents.ForEach(document =>
            {
                documentDTOs.Add(new DocumentDTO
                {
                    Id = document.Id,
                    Date = document.Date,
                    Note = document.Note
                });
            });
            return documentDTOs;
        }

        public async Task<DocumentDTO> Insert(DocumentRequestModel model)
        {
            var document = new Document
            {
                Date = model.Date,
                Note = model.Note,
                Customer = await _repositoryCustomer.FindById(model.CustomerId)
            };

            var result = await _repositoryDocument.Insert(document);

            var documentDTO = new DocumentDTO
            {
                Id = result.Id,
                Date = result.Date,
                Note = result.Note,
                Customer = new CustomerDTO
                {
                    Id = result.Customer.Id,
                    Adress = result.Customer.Adress,
                    Name = result.Customer.Name
                }
            };

            return documentDTO;

        }

        public async Task<DocumentDTO> InsertDocumentWithDetails(DocumentWithDetailsRequestModel model)
        {
            var document = new Document
            {
                Date = model.Date,
                Note = model.Note,
                Customer = await _repositoryCustomer.FindById(model.CustomerId)
            };

            List<DocumentDetails> documentsDetails = new List<DocumentDetails>();

            model.DocumentDetails.ForEach(documentDetail =>
            {
                documentsDetails.Add(new DocumentDetails
                {
                    ArticleName = documentDetail.ArticleName,
                    Price = documentDetail.Price,
                    Quantity = documentDetail.Quantity,
                    Document = document
                });
            });

            document.DocumentsDetails = documentsDetails;

            var result = await _repositoryDocument.InsertDocumentWithDetails(document);

            var documentDTO = new DocumentDTO
            {
                Id = result.Id,
                Date = result.Date,
                Note = result.Note,
                Customer = new CustomerDTO
                {
                    Id = result.Customer.Id,
                    Adress = result.Customer.Adress,
                    Name = result.Customer.Name
                }
             
            };

            List<DocumentDetailsDTO> documentDetailsDTOs = new List<DocumentDetailsDTO>();

            result.DocumentsDetails.ForEach(documentDetailsDTO =>
            {
                documentDetailsDTOs.Add(new DocumentDetailsDTO
                {
                    ArticleName = documentDetailsDTO.ArticleName,
                    Price = documentDetailsDTO.Price,
                    Quantity = documentDetailsDTO.Quantity
                });
            });

            documentDTO.DocumentsDetails = documentDetailsDTOs;

            return documentDTO;
        }

        public async Task<DocumentDTO> Update(DocumentRequestModel model, int id)
        {
            var document = await _repositoryDocument.FindById(id);
            document.Date = model.Date;
            document.Note = model.Note;
            document.Customer = await _repositoryCustomer.FindById(model.CustomerId);

            var result = await _repositoryDocument.Update(document);

            var documentDTO = new DocumentDTO
            {
                Id = result.Id,
                Date = result.Date,
                Note = result.Note,
                Customer = new CustomerDTO
                {
                    Id = result.Customer.Id,
                    Adress = result.Customer.Adress,
                    Name = result.Customer.Name
                }
            };

            return documentDTO;
        }
    }
}
