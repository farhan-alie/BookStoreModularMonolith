namespace BookStore.Books.Tests.Endpoints;

public class BookList(Fixture fixture) : TestBase<Fixture>
{
    [Fact]
    public async Task ReturnsThreeBooksAsync()
    {
        // Arrange


        // Act
        (HttpResponseMessage response, List.Response result) =
            await fixture.Client.GETAsync<List.Endpoint, List.Response>();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        result.Books.Count.Should().Be(3);
    }
}
