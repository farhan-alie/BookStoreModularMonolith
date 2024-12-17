using System.Security.Claims;
using BookStore.Users.UseCases.CartItems;

namespace BookStore.Users.CartEndpoints;

public static class List
{
    public record Response(List<CartItemDto> CartItems);

    internal sealed class Endpoint(IMediator mediator) : EndpointWithoutRequest<Response>
    {
        public override void Configure()
        {
            Get("/cart");
            Claims(ClaimTypes.Email);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            string? emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var query = new ListCartItems.Query(emailAddress!);

            Result<List<CartItemDto>> result = await mediator.Send(query, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync(ct);
            }
            else
            {
                await SendAsync(new Response(result.Value), cancellation: ct);
            }
        }
    }
}
