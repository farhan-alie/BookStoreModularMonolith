using Ardalis.Result;
using MediatR;

namespace BookStore.Contracts;

public record BookDetailsQuery(Guid BookId) : IRequest<Result<BookDetailsResponse>>;

public record BookDetailsResponse(
    Guid BookId,
    string Title,
    string Author,
    decimal Price);
