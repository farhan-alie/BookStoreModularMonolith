namespace BookStore.Books;

public class ListBooksResponse
{
    public IReadOnlyList<BookDto> Books { get; set; } = new List<BookDto>();
}   
