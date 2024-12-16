using System.Security.Claims;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;

namespace BookStore.Users.UsersEndpoints;

public static class Login
{
    public record Request(string Email, string Password);

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    internal sealed class Endpoint(UserManager<ApplicationUser> userManager) : Endpoint<Request>
    {
        public override void Configure()
        {
            Post("/users/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            bool result = await userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            string token = JwtBearer.CreateToken(
                options =>
                {
                    options.SigningKey = Config.GetValue<string>("Auth:JwtSecret")!;
                    options.User.Claims.Add(new Claim(ClaimTypes.Email, user.Email!));
                }
            );

            await SendOkAsync(token, ct);
        }
    }
}
