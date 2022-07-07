using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Business.Repositories.Data
{
    public class RepositoryDocumentDetails : IRepositoryDocumentDetails
    {
        private readonly TaskContext _context;

        public RepositoryDocumentDetails(TaskContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(DocumentDetails documentDetail)
        {
            _context.DocumentsDetails.Remove(documentDetail);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<DocumentDetails> FindById(int id)
        {
            return await _context.DocumentsDetails.FirstOrDefaultAsync(documentDetails => documentDetails.Id == id);
        }

        public async Task<List<DocumentDetails>> GetList()
        {
            return await _context.DocumentsDetails.ToListAsync();
        }

        public async Task<DocumentDetails> Insert(DocumentDetails documentDetails)
        {
            await _context.DocumentsDetails.AddAsync(documentDetails);

            await _context.SaveChangesAsync();

            return documentDetails;
        }

        public async Task<DocumentDetails> Update(DocumentDetails documentDetails)
        {
            _context.DocumentsDetails.Update(documentDetails);

            await _context.SaveChangesAsync();

            return documentDetails;
        }
    }
}
