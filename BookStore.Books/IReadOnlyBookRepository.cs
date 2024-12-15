namespace BookStore.Books;

public partial interface IBookRepository
{
    Task<List<Book>> ListAsync(CancellationToken cancellationToken = default);
    Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
