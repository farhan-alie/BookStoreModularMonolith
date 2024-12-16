using Ardalis.GuardClauses;

namespace BookStore.Users;

public class ApplicationUser : IdentityUser
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        CartItem? existingBook = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
        if (existingBook != null)
        {
            existingBook.Update(existingBook.Quantity + item.Quantity, item.Description, item.UnitPrice);
            return;
        }

        _cartItems.Add(item);
    }
}
