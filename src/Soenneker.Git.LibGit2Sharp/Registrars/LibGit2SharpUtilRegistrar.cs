using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Git.LibGit2Sharp.Abstract;
using Soenneker.Utils.Directory.Registrars;
using Soenneker.Utils.Process.Registrars;

namespace Soenneker.Git.LibGit2Sharp.Registrars;

/// <summary>
/// A wrapper around LibGit2Sharp
/// </summary>
public static class LibGit2SharpUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ILibGit2SharpUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddLibGit2SharpUtilAsSingleton(this IServiceCollection services)
    {
        services.AddProcessUtilAsSingleton().AddDirectoryUtilAsSingleton().TryAddSingleton<ILibGit2SharpUtil, LibGit2SharpUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ILibGit2SharpUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddLibGit2SharpUtilAsScoped(this IServiceCollection services)
    {
        services.AddProcessUtilAsScoped().AddDirectoryUtilAsScoped().TryAddScoped<ILibGit2SharpUtil, LibGit2SharpUtil>();

        return services;
    }
}
