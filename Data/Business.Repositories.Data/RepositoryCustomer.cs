using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Business.Repositories.Data
{
    public class RepositoryCustomer : IRepositoryCustomer
    {
        private readonly TaskContext _context;

        public RepositoryCustomer(TaskContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var customer = await FindById(id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Customer customer)
        {
            _context.Customers.Remove(customer);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Customer> FindById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task<Customer> GetCustomerWithDocuments(int id)
        {
            return await _context.Customers.Include(customer => customer.Documents)
                                    .ThenInclude(document => document.DocumentsDetails)                   
                                    .FirstOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task<Customer> GetCustomerWithDocument(int customerId, int documentId)
        {
            return await _context.Customers.Include(customer => customer.Documents.Where(document => document.Id == documentId))
                                    .ThenInclude(document => document.DocumentsDetails)
                                    .FirstOrDefaultAsync(customer => customer.Id == customerId);
        }

        public async Task<List<Customer>> GetList()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> Insert(Customer customer)
        {
            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
