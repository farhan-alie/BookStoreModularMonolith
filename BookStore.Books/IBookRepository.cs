namespace BookStore.Books;

public partial interface IBookRepository
{
    Task AddAsync(Book book);
    Task DeleteAsync(Book book);
    Task SaveChangesAsync();
}
