using Azure.Storage.Blobs;
using Nandun.Reference.WorkerFunction.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nandun.Reference.WorkerFunction.Extensions;

await new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureLogging(builder =>
    {
        builder.AddApplicationInsights(cfg =>
            {
                cfg.ConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
            },
            _ => { });
    })
    .ConfigureServices(services =>
    {
        services.ConfigureFunctionsApplicationInsights();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddAzureClients(b =>
        {
            b.AddBlobServiceClient(Environment.GetEnvironmentVariable(nameof(IWorkerFunctionSettings.ApplicationStorage)))
                .WithName(nameof(IWorkerFunctionSettings.ApplicationStorage));
        });

        // Use this for typed app settings.
        services.AddSingleton<IWorkerFunctionSettings, WorkerFunctionSettings>();
    })
    .Build()
    .Initialize(provider =>
    {
        // Null suppressed! because these will never be null, cos we're just setting it up above.
        IAzureClientFactory<BlobServiceClient> factory = provider.GetService<IAzureClientFactory<BlobServiceClient>>()!;
        IWorkerFunctionSettings settings = provider.GetService<IWorkerFunctionSettings>()!;

        BlobServiceClient client = factory.CreateClient(nameof(settings.ApplicationStorage));
        client.GetBlobContainerClient(settings.RawDataContainerName)
            .CreateIfNotExists();
    })
    .RunAsync();

