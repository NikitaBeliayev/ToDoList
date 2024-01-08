using System.Reflection.Metadata;
using Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Document = Domain.Documents.Document;

namespace Infrastructure.Database.Configuration;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.AccessStatus)
            .HasConversion(
                value => Enum.GetName(value.GetType(), value),
                value => (AccessStatus)Enum.Parse(typeof(AccessStatus), value));

        builder.Property(e => e.Title)
            .HasConversion(value => value.Value,
                value => new Title()
                {
                    Value = value
                });
    }
}