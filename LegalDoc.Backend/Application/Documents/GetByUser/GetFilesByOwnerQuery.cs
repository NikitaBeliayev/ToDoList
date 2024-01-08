using System.Reflection.Metadata;
using Application.Abstraction.Messaging;
using Application.Documents.RequestDto;
using Domain.Documents;
using Document = Domain.Documents.Document;

namespace Application.Documents.GetByUser;

public record GetFilesByOwnerQuery(GetDocumentByOwnerDto DocumentByOwnerDto) : IQuery<IEnumerable<Document>>
{
    
}