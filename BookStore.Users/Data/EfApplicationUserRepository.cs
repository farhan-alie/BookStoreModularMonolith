using Microsoft.EntityFrameworkCore;

namespace BookStore.Users.Data;

public class EfApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
    public async Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return await dbContext.ApplicationUsers
            .Include(user => user.CartItems)
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
