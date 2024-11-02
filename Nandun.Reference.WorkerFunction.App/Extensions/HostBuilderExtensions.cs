using Microsoft.Extensions.Hosting;

namespace Nandun.Reference.WorkerFunction.Extensions;
/// <summary>
/// Extensions for <see cref="IHost"/>
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Execute custom initialization code just before running a host, for e.g. setting up a blob container or something.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="initializer"></param>
    /// <returns></returns>
    public static IHost Initialize(this IHost host, Action<IServiceProvider> initializer)
    {
        initializer(host.Services);
        return host;
    }

}