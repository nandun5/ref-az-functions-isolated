using Nandun.Reference.WorkerFunction.Bindings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Nandun.Reference.WorkerFunction.Extensions;
using Nandun.Reference.WorkerFunction.Settings;

namespace Nandun.Reference.WorkerFunction.Functions;

/// <summary>
/// Generic Data ingest API for reference
/// </summary>
public class HttpGenericDataFunction
{
    private readonly ILogger<HttpGenericDataFunction> _logger;

    /// <summary>
    /// Default DI constructor
    /// </summary>
    /// <param name="logger"></param>
    public HttpGenericDataFunction(ILogger<HttpGenericDataFunction> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// HTTP Trigger for generic data ingest
    /// </summary>
    /// <param name="req"></param>
    /// <param name="client"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    [Function("http-v1-generic-data")]
    public async Task<GenericDataOutput?> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/{source}/data")]
        HttpRequestData req,
        [BlobInput($"%{nameof(IWorkerFunctionSettings.RawDataContainerName)}%/{{rand-guid}}", Connection = nameof(IWorkerFunctionSettings.ApplicationStorage))] BlobClient client,
        string source)
    {
        if (source.Contains(' '))
        {
            req.BadRequest($"Source cannot have spaces: {source}");
            return default;
        }

        // HINT: Blob name is {rand-guid} from the binding pipeline. Let's slap it on as our tracking key. 
        string trackingKey = client.Name;
        DateTimeOffset asOf = DateTimeOffset.UtcNow;
        _logger.LogInformation(
            $"[WorkerFunction Data][TrackingKey={trackingKey}] data received from [Source={source}]. with [Headers={req.Headers.ToString().Replace("\r\n", "; ")}]");

        BasicQueueMessage queueMessage = new(source, trackingKey, asOf, req.Headers, trackingKey);

        await client.UploadAsync(req.Body);

        await req.Accepted().AddHeader("Tracking-Key", trackingKey)
            .WriteStringAsync(trackingKey);

        GenericDataOutput output = new(
            queueMessage,
            queueMessage
        );

        return output;
    }
}