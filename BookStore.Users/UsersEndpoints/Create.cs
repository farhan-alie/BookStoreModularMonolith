namespace BookStore.Users.UsersEndpoints;

public static class Create
{
    public record Request(string Email, string Password, string FullName);

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
            Post("/users");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct)
        {
            var newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FullName = request.FullName
            };

            await userManager.CreateAsync(newUser, request.Password);
            await SendOkAsync(ct);
        }
    }
}
