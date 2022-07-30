using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(book => book.FeedBacks)
                .WithOne()
                .HasForeignKey(feedback => feedback.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(book => book.Authors)
                .WithOne()
                .HasForeignKey(author => author.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
