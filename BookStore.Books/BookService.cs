namespace BookStore.Books;

internal sealed class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<BookDto>> ListBooksAsync()
    {
        return (await bookRepository.ListAsync())
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price))
            .ToList();
    }

    public async Task<BookDto> GetBookAsync(Guid id)
    {
        Book book = (await bookRepository.GetAsync(id))!;
#pragma warning disable S1135
        // TODO: handle not found case
#pragma warning restore S1135
        return new BookDto(book.Id, book.Title, book.Author, book.Price);
    }

    public async Task CreateBookAsync(BookDto book)
    {
        var newBook = new Book(book.Id, book.Title, book.Author, book.Price);
        await bookRepository.AddAsync(newBook);
        await bookRepository.SaveChangesAsync();
    }

    public async Task UpdateBookPriceAsync(Guid id, decimal price)
    {
        Book book = (await bookRepository.GetAsync(id))!;
#pragma warning disable S1135
        // TODO: handle not found case
#pragma warning restore S1135
        book.UpdatePrice(price);
        await bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        Book? book = await bookRepository.GetAsync(id);
        if (book is not null)
        {
            await bookRepository.DeleteAsync(book);
            await bookRepository.SaveChangesAsync();
        }
    }
}
