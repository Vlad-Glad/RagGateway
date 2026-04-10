using CorporateRAG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorporateRAG.Infrastructure.Persistence.Configuration;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.StoragePath)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.UploadedAtUtc)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();
    }
}