using Domain.Documents;
using Shared;

namespace Domain.Users;

public class User : IEntity
{
    public Guid Id { get; set; }
    public Name FirstName { get; set; }
    public Name LastName { get; set; }
    public EmailAddress EmailAddress { get; set; }
    public Password Password { get; set; }
    public ICollection<Document> Documents { get; }= new List<Document>();
}