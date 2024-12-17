using BookStore.Contracts;

namespace BookStore.Users.UseCases.CartItems;

public static class AddCartItem
{
    public record Command(Guid BookId, int Quantity, string EmailAddress)
        : IRequest<Result>;

    internal sealed class Handler(IApplicationUserRepository userRepository, IMediator mediator)
        : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            var query = new BookDetailsQuery(request.BookId);
            Result<BookDetailsResponse> result = await mediator.Send(query, cancellationToken);

            if (result.Status == ResultStatus.NotFound)
            {
                return Result.NotFound();
            }

            BookDetailsResponse? bookDetails = result.Value;

            string description = $"{bookDetails.Title} by {bookDetails.Author}";
            var newCartItem = new CartItem(request.BookId, description, request.Quantity, bookDetails.Price);
            user.AddItemToCart(newCartItem);
            await userRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
