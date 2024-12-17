namespace BookStore.Users.UseCases.CartItems;

public record CartItemDto(
    Guid Id,
    Guid BookId,
    string Description,
    int Quantity,
    decimal UnitPrice);
