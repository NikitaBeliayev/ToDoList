using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Domain.Users;
using Shared;

namespace Application.Users.GetById;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetByIdAsync(query.UserId);

        return result != null
            ? Result<UserDto>.Success(new UserDto()
            {
                Id = result.Id,
                EmailAddress = result.EmailAddress.Value,
                FirstName = result.FirstName.Value,
                LastName = result.LastName.Value,
                Password = result.Password.Value
            })
            : Result<UserDto>.Success(null);
    }

}