using Application.Abstraction.Messaging;
using Application.Documents.RequestDto;

namespace Application.Documents.PostDocument;

public record PostDocumentCommand(PostDocumentDto DocumentDto, Stream FileStream, string FileName) : ICommand<PostDocumentDto>
{
    
}