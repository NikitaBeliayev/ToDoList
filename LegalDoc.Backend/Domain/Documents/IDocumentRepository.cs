namespace Domain.Documents;

public interface IDocumentRepository
{
    Task<IEnumerable<Document>> GetAllDocumentsByOwnerEmail(string emailAddress);
    Task<Document?> PostDocument(Document document);
}