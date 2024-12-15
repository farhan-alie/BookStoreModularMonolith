namespace BookStore.Books.Endpoints.Books;

public static class GetById
{
    public record Request(Guid Id);

    internal sealed class Endpoint(IBookService bookService) : Endpoint<Request, BookDto>
    {
        public override void Configure()
        {
            Get("books/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct)
        {
            BookDto book = await bookService.GetBookAsync(request.Id);
            await SendAsync(book, cancellation: ct);
        }
    }
}
