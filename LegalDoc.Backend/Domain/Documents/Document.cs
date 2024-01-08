using Domain.Users;
using Shared;

namespace Domain.Documents;

public class Document : IEntity
{
    public Guid Id { get; set; }
    public Title Title { get; set; }
    public Guid OwnerId { get; set; }
    public AccessStatus AccessStatus { get; set; }
    public ICollection<User> Users { get; } = new List<User>();
}