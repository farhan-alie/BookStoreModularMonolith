using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Books.Data;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        builder.HasData(GetSampleBookData());
    }

    internal static readonly Guid Book1Guid = new("A89F6CD7-4693-457B-9009-02205DBBFE45");
    internal static readonly Guid Book2Guid = new("E4FA19BF-6981-4E50-A542-7C9B26E9EC31");
    internal static readonly Guid Book3Guid = new("17C61E41-3953-42CD-8F88-D3F698869B35");

    private static IEnumerable<Book> GetSampleBookData()
    {
        string tolkien = "J.R.R. Tolkien";
        yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
        yield return new Book(Book2Guid, "The Two Towers", tolkien, 11.99m);
        yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
    }
}
