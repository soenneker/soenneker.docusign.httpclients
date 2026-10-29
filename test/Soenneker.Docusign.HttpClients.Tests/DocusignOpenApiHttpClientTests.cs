using Soenneker.Docusign.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Docusign.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class DocusignOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IDocusignOpenApiHttpClient _httpclient;

    public DocusignOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IDocusignOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
