namespace BookStore.Books.Endpoints.Books;

public static class Delete
{
    public record Request(Guid Id);

    internal sealed class Endpoint(IBookService bookService) : Endpoint<Request, BookDto>
    {
        public override void Configure()
        {
            Delete("books/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            await bookService.DeleteBookAsync(req.Id);
            await SendNoContentAsync(ct);
        }
    }
}
