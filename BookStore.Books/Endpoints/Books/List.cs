namespace BookStore.Books.Endpoints.Books;

public static class List
{
    internal sealed record Response(List<BookDto> Books);

    internal sealed class Endpoint(IBookService bookService) : EndpointWithoutRequest<Response>
    {
        public override void Configure()
        {
            Get("books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            List<BookDto> books = await bookService.ListBooksAsync();

            await SendAsync(new Response(books), cancellation: ct);
        }
    }
}
