using Domain.Documents;
using Microsoft.AspNetCore.Http;

namespace Application.Documents.RequestDto;

public class PostDocumentDto
{
    public string EmailAddress { get; set; }
    public IFormFile File { get; set; }
    public AccessStatus AccessStatus { get; set; }
    public decimal Price { get; set; }
}