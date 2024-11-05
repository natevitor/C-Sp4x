
using Xunit;

[CollectionDefinition("Integration Tests")]
public class TestFixture : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
}