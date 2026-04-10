using CorporateRAG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorporateRAG.Infrastructure.Persistence.Configuration;

public class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
{
    public void Configure(EntityTypeBuilder<DocumentChunk> builder)
    {
        builder.ToTable("document_chunks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DocumentId)
            .IsRequired();

        builder.Property(x => x.ChunkIndex)
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired();

        builder.Property(x => x.PageNumber);

        builder.HasIndex(x => new { x.DocumentId, x.ChunkIndex })
            .IsUnique();
    }
}