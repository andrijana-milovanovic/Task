using Data.Business.Repositories;
using Models.DTOs;
using Models.Entities;
using Models.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.Data
{
    public class ServiceCustomer : IServiceCustomer
    {
        private readonly IRepositoryCustomer _repositoryCustomer;

        public ServiceCustomer(IRepositoryCustomer repositoryCustomer)
        {
            _repositoryCustomer = repositoryCustomer;
        }

        public async Task<CustomerDTO> FindById(int id)
        {
            var customer = await _repositoryCustomer.FindById(id);

            var customerDTO = new CustomerDTO
            {
                Id = id,
                Name = customer.Name,
                Adress = customer.Adress
            };

            return customerDTO;
        }

        public async Task<List<CustomerDTO>> GetList()
        {
            var customers = await _repositoryCustomer.GetList();

            if(customers is null)
            {
                return null;
            }

            var customerDTOs = CustomerEntityToDTO(customers);
            
            return customerDTOs;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await _repositoryCustomer.FindById(id);

            if (customer == null)
            {
                return false;
            }

            return await _repositoryCustomer.Delete(customer);
        }

        public async Task<CustomerDTO> Insert(CustomerRequestModel model)
        {
            var customer = new Customer
            {
                Name = model.Name,
                Adress = model.Adress
            };

            var result = await _repositoryCustomer.Insert(customer);

            var customerDTO = new CustomerDTO
            {
                Id = result.Id,
                Name = result.Name,
                Adress = result.Adress
            };

            return customerDTO;
        }

        public async Task<CustomerDTO> Update(CustomerRequestModel model, int id)
        {
            var customer = await _repositoryCustomer.FindById(id);

            customer.Name = model.Name;
            customer.Adress = model.Adress;

            var result = await _repositoryCustomer.Update(customer);

            var customerDTO = new CustomerDTO
            {
                Id = result.Id,
                Name = result.Name,
                Adress = result.Adress
            };

            return customerDTO;
        }

        public async Task<DocumentViewModel> GetDocumentForCustomer(int customerId, int documentId)
        {
            var customer = await _repositoryCustomer.GetCustomerWithDocument(customerId, documentId);

            if (customer == null || !customer.Documents.Any()) 
            {
                return null;
            }

            var documentViewModel = CreateDocumentViewModel(customer);

            return documentViewModel;
        }

        public async Task<List<DocumentViewModel>> GetAllDocumentsForCustomer(int id)
        {
            var customer = await _repositoryCustomer.GetCustomerWithDocuments(id);

            var documentViewModelList = CreateDocumentViewModelList(customer);

            return documentViewModelList;
        }

        private List<DocumentViewModel> CreateDocumentViewModelList(Customer customer)
        {
            var customerDTO = new CustomerDTO
            {
                Name = customer.Name,
                Adress = customer.Adress,
                Id = customer.Id
            };

            var documentViewModelList = new List<DocumentViewModel>();

            customer.Documents.ForEach(document =>
            {
                List<DocumentDetailsDTO> documentDetailsDTOs = new List<DocumentDetailsDTO>();

                document.DocumentsDetails.ForEach(documentDetail =>
                {
                    documentDetailsDTOs.Add(new DocumentDetailsDTO
                    {
                        Id = documentDetail.Id,
                        ArticleName = documentDetail.ArticleName,
                        Price = documentDetail.Price,
                        Quantity = documentDetail.Quantity,
                        DocumentId = documentDetail.DocumentId
                    });
                });

                DocumentViewModel documentViewModel = new DocumentViewModel
                {
                    Id = document.Id,
                    Note = document.Note,
                    Date = document.Date,
                    Customer = customerDTO,
                    DocumentsDetails = documentDetailsDTOs
                };

                documentViewModelList.Add(documentViewModel);
            });

            return documentViewModelList;
        }

        private List<CustomerDTO> CustomerEntityToDTO(List<Customer> customers)
        {
            var customerDTOs = new List<CustomerDTO>();

            customers.ForEach(customer =>
            {
                customerDTOs.Add(new CustomerDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Adress = customer.Adress
                });
            });
            return customerDTOs;
        }

        private DocumentViewModel CreateDocumentViewModel(Customer customer)
        {
            var customerDTO = new CustomerDTO
            {
                Name = customer.Name,
                Adress = customer.Adress,
                Id = customer.Id
            };

            List<DocumentDetailsDTO> documentDetailsDTOs = new List<DocumentDetailsDTO>();

            var document = customer.Documents.FirstOrDefault();

            document.DocumentsDetails.ForEach(documentDetail =>
            {
                documentDetailsDTOs.Add(new DocumentDetailsDTO
                {
                    Id = documentDetail.Id,
                    ArticleName = documentDetail.ArticleName,
                    Price = documentDetail.Price,
                    Quantity = documentDetail.Quantity,
                    DocumentId = documentDetail.DocumentId
                });
            });

            var documentViewModel = new DocumentViewModel
            {
                Id = document.Id,
                Note = document.Note,
                Date = document.Date,
                Customer = customerDTO,
                DocumentsDetails = documentDetailsDTOs
            };

            return documentViewModel;
        }
    }
}
