using System.Linq.Expressions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);

        return user;
    }

    public async Task<User?> GetUserByExpressionAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

}