using Soenneker.Tests.HostedUnit;

namespace Soenneker.Keap.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class KeapOpenApiClientTests : HostedUnitTest
{
    public KeapOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
