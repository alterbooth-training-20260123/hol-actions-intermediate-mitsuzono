using System.Net;

namespace SampleWebAppForActions.Test;

public sealed class RazorPagesSmokeTests : IClassFixture<SampleWebAppFactory>
{
    private readonly SampleWebAppFactory _factory;

    public RazorPagesSmokeTests(SampleWebAppFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/", "Welcome")]
    [InlineData("/Privacy", "Privacy Policy")]
    public async Task Get_Pages_ReturnsOk_AndExpectedContent(string path, string expectedText)
    {
        using var client = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            AllowAutoRedirect = false,
        });

        using var response = await client.GetAsync(path);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var html = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedText, html);
    }
}
