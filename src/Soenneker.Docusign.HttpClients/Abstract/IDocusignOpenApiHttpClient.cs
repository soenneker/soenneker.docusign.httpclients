using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Docusign.HttpClients.Abstract;

/// <summary>
/// Provides a cached HTTP transport for Docusign eSignature. Authentication is applied by the OpenAPI client utility.
/// </summary>
public interface IDocusignOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the shared HTTP client. Configure Docusign:ClientBaseUrl with the account's base_uri plus /restapi.
    /// Defaults to the demo environment. This transport does not attach OAuth credentials.</summary>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}

