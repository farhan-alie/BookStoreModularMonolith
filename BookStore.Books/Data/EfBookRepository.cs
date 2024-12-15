using Microsoft.EntityFrameworkCore;

namespace BookStore.Books.Data;

public class EfBookRepository(BooksDbContext dbContext) : IBookRepository
{
    public async Task<List<Book>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Books.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Books.FindAsync([id], cancellationToken);
    }

    public Task AddAsync(Book book)
    {
        dbContext.Books.Add(book);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Book book)
    {
        dbContext.Books.Remove(book);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
