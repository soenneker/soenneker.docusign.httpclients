using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Docusign.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Docusign.HttpClients;

public sealed class DocusignOpenApiHttpClient : IDocusignOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private const string _defaultBaseUrl = "https://demo.docusign.net/restapi";

    public DocusignOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(DocusignOpenApiHttpClient), (config: _config, baseUrl: _config["Docusign:ClientBaseUrl"] ?? _defaultBaseUrl), static state =>
        {
            if (!Uri.TryCreate(state.baseUrl, UriKind.Absolute, out Uri? baseAddress) ||
                baseAddress.Scheme != Uri.UriSchemeHttps || !string.IsNullOrEmpty(baseAddress.UserInfo) ||
                !string.IsNullOrEmpty(baseAddress.Query) || !string.IsNullOrEmpty(baseAddress.Fragment) ||
                !baseAddress.AbsolutePath.TrimEnd('/').Equals("/restapi", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Docusign:ClientBaseUrl must be an HTTPS account base URI ending in /restapi.");

            return new HttpClientOptions
            {
                BaseAddress = new Uri(baseAddress.AbsoluteUri.TrimEnd('/') + "/"),
                DefaultRequestHeaders = new Dictionary<string, string> { ["Accept"] = "application/json" }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(nameof(DocusignOpenApiHttpClient));
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(nameof(DocusignOpenApiHttpClient));
    }
}

