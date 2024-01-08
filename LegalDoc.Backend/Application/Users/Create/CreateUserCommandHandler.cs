using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Domain.Users;
using Shared;

namespace Application.Users.Create;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    private IUnitOfWork _unitOfWork { get; set; }
    private IUserRepository _userRepository { get; set; }
    
    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = EmailAddress.BuildEmail(request.User.EmailAddress);
        if (!email.IsSuccess)
        {
            return Result<UserDto>.Failure(request.User, new Error("",""));
        }

        var firstName = Name.BuildName(request.User.FirstName);
        if (!firstName.IsSuccess)
        {
            return Result<UserDto>.Failure(request.User, new Error("", ""));
        }

        var lastName = Name.BuildName(request.User.LastName);
        if (!lastName.IsSuccess)
        {
            return Result<UserDto>.Failure(request.User, new Error("", ""));
        }

        var password = Password.BuildPassword(request.User.Password);
        if (!password.IsSuccess)
        {
            return Result<UserDto>.Failure(request.User, new Error("", ""));
        }

        User newUser = new()
        {
            Id = Guid.NewGuid(),
            EmailAddress = email.Value!,
            FirstName = firstName.Value!,
            LastName = lastName.Value!,
            Password = password.Value!
        };

        var result = await _userRepository.CreateAsync(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result != null)
        {
            return Result<UserDto>.Success(new UserDto()
            {
                Id = result.Id,
                EmailAddress = result.EmailAddress.Value,
                FirstName = result.FirstName.Value,
                LastName = result.LastName.Value,
                Password = result.Password.Value
            });
        }

        return Result<UserDto>.Failure(request.User, new Error("", ""));

    }
}
