using Application.Abstraction.Messaging;
using Domain.Documents;
using Shared;

namespace Application.Documents.GetByUser;

public class GetFilesByOwnerQueryHandler : IQueryHandler<GetFilesByOwnerQuery, IEnumerable<Document>>
{
    private readonly IDocumentRepository _documentRepository;

    public GetFilesByOwnerQueryHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }
    public async Task<Result<IEnumerable<Document>>> Handle(GetFilesByOwnerQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Document> documents = await _documentRepository.GetAllDocumentsByOwnerEmail(request.DocumentByOwnerDto.OwnerEmailAddress);
        return Result<IEnumerable<Document>>.Success(documents);
    }
}