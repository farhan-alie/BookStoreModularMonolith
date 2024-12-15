using FastEndpoints;
using FluentValidation;

namespace BookStore.Books.Endpoints.Books;

public static class UpdatePrice
{
    public record Request(Guid Id, decimal NewPrice);

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewPrice).GreaterThan(0);
        }
    }

    internal sealed class Endpoint(IBookService bookService) : Endpoint<Request, BookDto>
    {
        public override void Configure()
        {
            Post("books/{Id}/pricehistory");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            await bookService.UpdateBookPriceAsync(req.Id, req.NewPrice);
            BookDto book = await bookService.GetBookAsync(req.Id);
            await SendAsync(book, cancellation: ct);
        }
    }
}
