using FastEndpoints;
using FluentValidation;

namespace BookStore.Books.Endpoints.Books;

public static class Create
{
    public record Request(string Title, string Author, decimal Price);

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }

    internal sealed class Endpoint(IBookService bookService) : Endpoint<Request, BookDto>
    {
        public override void Configure()
        {
            Post("books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var dto = new BookDto(Guid.CreateVersion7(), req.Title, req.Author, req.Price);
            await bookService.CreateBookAsync(dto);
            await SendCreatedAtAsync<GetById.Endpoint>(new GetById.Request(dto.Id), dto, cancellation: ct);
        }
    }
}
