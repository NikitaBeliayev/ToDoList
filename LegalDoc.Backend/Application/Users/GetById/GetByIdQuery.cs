using Application.Abstraction.Messaging;

namespace Application.Users.GetById;

public record GetByIdQuery(Guid UserId) : IQuery<UserDto>
{
    
}