using Ardalis.GuardClauses;

namespace BookStore.Users;

public class CartItem
{
    public CartItem()
    {
        // Required by EF
    }

    public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
    {
        BookId = Guard.Against.Default(bookId);
        Description = Guard.Against.NullOrEmpty(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid BookId { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public void Update(int quantity, string description, decimal unitPrice)
    {
        Quantity = Guard.Against.Negative(quantity);
        Description = Guard.Against.NullOrEmpty(description);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }
}
