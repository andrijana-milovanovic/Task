using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Business.Repositories.Data
{
    public class RepositoryDocument : IRepositoryDocument
    {
        private readonly TaskContext _context;

        public RepositoryDocument(TaskContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(Document document)
        {
            _context.Documents.Remove(document);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Document> FindById(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(document => document.Id == id);
        }

        public async Task<List<Document>> GetList()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> Insert(Document document)
        {
            await _context.Documents.AddAsync(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<Document> InsertDocumentWithDetails(Document document)
        {
            await _context.Documents.AddAsync(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<Document> Update(Document document)
        {
            _context.Documents.Update(document);

            await _context.SaveChangesAsync();

            return document;
        }
    }
}
