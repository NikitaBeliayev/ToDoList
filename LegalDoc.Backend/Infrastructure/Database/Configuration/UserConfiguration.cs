using Domain.Intermediate;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => Name.BuildName(value).Value!
            )
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => Name.BuildName(value).Value!
            )
            .IsRequired();

        builder.Property(p => p.EmailAddress)
            .HasConversion(
                emailAddress => emailAddress.Value,
                value => EmailAddress.BuildEmail(value).Value!
            )
            .IsRequired();

        builder.Property(p => p.Password)
            .HasConversion(
                password => password.Value,
                value => Password.BuildPassword(value).Value!
            )
            .IsRequired();

        builder.HasMany(e => e.Documents)
            .WithMany(e => e.Users)
            .UsingEntity<UserDocument>();
    }
}
