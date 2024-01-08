using Application.Documents.GetByUser;
using Application.Documents.PostDocument;
using Application.Documents.RequestDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LegalDoc.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly ISender _sender;

    public DocumentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllDocumentsByOwner([FromQuery] GetDocumentByOwnerDto documentByOwnerDto, CancellationToken cancellationToken)
    {
        var query = new GetFilesByOwnerQuery(documentByOwnerDto);
        var result = await _sender.Send(query, cancellationToken);
        return null;
    }
    
    [HttpPost("Upload")]
    public async Task<IActionResult> PostDocument([FromForm] PostDocumentDto documentDto, CancellationToken cancellationToken)
    {
        var command = new PostDocumentCommand(documentDto, documentDto.File.OpenReadStream(), documentDto.File.Name);
        
        var result = await _sender.Send(command, cancellationToken);
        return new ObjectResult(result);
    }
}