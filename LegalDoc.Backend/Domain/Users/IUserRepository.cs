using System.Linq.Expressions;

namespace Domain.Users;

public interface IUserRepository
{
    public Task<User?> CreateAsync(User user);
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<User?> GetUserByExpressionAsync(Expression<Func<User, bool>> expression,
        CancellationToken cancellationToken = default);

}