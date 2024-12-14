using FastEndpoints;

namespace BookStore.Books;

internal sealed class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        List<BookDto> books = bookService.ListBooks();
        return SendAsync(new ListBooksResponse
        {
            Books = books
        }, cancellation: ct);
    }
}  
