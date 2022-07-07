using Models.Entities;
using System.Threading.Tasks;

namespace Data.Business.Repositories
{
    public interface IRepositoryDocument : IRepository<Document>
    {
        Task<Document> InsertDocumentWithDetails(Document document);
    }
}
