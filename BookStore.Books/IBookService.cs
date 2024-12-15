namespace BookStore.Books;

internal interface IBookService
{
    Task<List<BookDto>> ListBooksAsync();
    Task<BookDto> GetBookAsync(Guid id);
    Task CreateBookAsync(BookDto book);
    Task UpdateBookPriceAsync(Guid id, decimal price);
    Task DeleteBookAsync(Guid id);
}
