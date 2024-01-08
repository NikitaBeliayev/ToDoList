using Domain.Documents;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _dbContext;
    public DocumentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Document>> GetAllDocumentsByOwnerEmail(string emailAddress)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(user => user.EmailAddress == EmailAddress.BuildEmail(emailAddress).Value);
        Guid ownerId = user.Id;
        IEnumerable<Document> documents = _dbContext.Documents.Where(document => document.OwnerId == ownerId).AsEnumerable();
        return documents;
    }
    

    public async Task<Document?> PostDocument(Document document)
    {
         await _dbContext.Documents.AddAsync(document);
         return document;
    }
}