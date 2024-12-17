using System.Security.Claims;
using BookStore.Users.UseCases.CartItems;

namespace BookStore.Users.CartEndpoints;

public static class Add
{
    public record Request(Guid BookId, int Quantity);

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }

    internal sealed class Endpoint(IMediator mediator) : Endpoint<Request>
    {
        public override void Configure()
        {
            Post("/cart");
            Claims(ClaimTypes.Email);
        }

        public override async Task HandleAsync(Request request, CancellationToken ct)
        {
            string? emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var command = new AddCartItem.Command(request.BookId, request.Quantity, emailAddress!);

            Result result = await mediator.Send(command, ct);

            if (result.Status == ResultStatus.Unauthorized)
            {
                await SendUnauthorizedAsync(ct);
            }
            else
            {
                await SendOkAsync(ct);
            }
        }
    }
}
