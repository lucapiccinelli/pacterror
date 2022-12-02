using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using consolex86;
using PactNet;
using Xunit;

namespace Consolex86.Test;

public class PactTest
{
    private readonly IPactBuilderV3 _pactBuilder;

    public PactTest()
    {
        _pactBuilder = Pact
            .V3("G3p Token requestAPI Consumer", "G3p Token requestAPI Consumer", new PactConfig())
            .WithHttpInteractions();
    }

    [Fact]
    public async Task Test1()
    {
        _pactBuilder
            .UponReceiving("A GET request")
            .Given("a get call")
                .WithRequest(HttpMethod.Get, "/test")
            .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithBody("OK", "text/plain")
            ;

        await _pactBuilder.VerifyAsync(async context =>
        {
            string result = await MyService.GetAsync($"{context.MockServerUri}test");
            Assert.Equal("OK", result);
        });
    }
}