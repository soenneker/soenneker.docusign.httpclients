using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Docusign.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Docusign.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class DocusignOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="DocusignOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddDocusignOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IDocusignOpenApiHttpClient, DocusignOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="DocusignOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddDocusignOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IDocusignOpenApiHttpClient, DocusignOpenApiHttpClient>();

        return services;
    }
}
