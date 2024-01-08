using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LegalDoc.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(userId);
        var result = await _sender.Send(query, cancellationToken);
        return new ObjectResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto userDto, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(userDto);
        var result = await _sender.Send(command, cancellationToken);
        return new ObjectResult(result);
    }
}