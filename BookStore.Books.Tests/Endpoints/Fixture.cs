using Xunit.Abstractions;

namespace BookStore.Books.Tests.Endpoints;

#pragma warning disable CA1515
public class Fixture(IMessageSink messageSink) : AppFixture<Program>(messageSink)
#pragma warning restore CA1515
{
    protected override Task SetupAsync()
    {
        Client = CreateClient();
        return Task.CompletedTask;
    }

    protected override Task TearDownAsync()
    {
        Client.Dispose();
        return base.TearDownAsync();
    }
}
