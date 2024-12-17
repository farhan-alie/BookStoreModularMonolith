namespace BookStore.Users.UseCases.CartItems;

public static class ListCartItems
{
    public sealed record Query(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;

    internal sealed class Handler(IApplicationUserRepository userRepository)
        : IRequestHandler<Query,
            Result<List<CartItemDto>>>
    {
        public async Task<Result<List<CartItemDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

            if (user is null)
            {
                return Result.Unauthorized();
            }

            var cartItems = user.CartItems.Select(ci => new CartItemDto
                (ci.Id, ci.BookId, ci.Description, ci.Quantity, ci.UnitPrice)).ToList();

            return cartItems;
        }
    }
}
