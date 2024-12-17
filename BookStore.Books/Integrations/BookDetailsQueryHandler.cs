using Ardalis.Result;
using BookStore.Contracts;
using MediatR;

namespace BookStore.Books.Integrations;

internal sealed class BookDetailsQueryHandler(IBookService bookService)
    : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
    public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
    {
        BookDto? bookDetails = await bookService.GetBookAsync(request.BookId);
        return new BookDetailsResponse(bookDetails.Id, bookDetails.Title, bookDetails.Author, bookDetails.Price);
    }
}
