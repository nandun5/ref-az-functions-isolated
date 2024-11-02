using Nandun.Reference.WorkerFunction.Functions;
using Nandun.Reference.WorkerFunction.Settings;
using Microsoft.Azure.Functions.Worker;

namespace Nandun.Reference.WorkerFunction.Bindings;

/// <summary>
/// Output binding of the <see cref="HttpGenericDataFunction.RunAsync"/> endpoint
/// </summary>
public class GenericDataOutput
{
    /// <summary>
    /// Required fields
    /// </summary>
    /// <param name="eventProcessorQueueMessage"></param>
    /// <param name="messageProcessorQueueMessage"></param>
    public GenericDataOutput( 
        BasicQueueMessage eventProcessorQueueMessage,
        BasicQueueMessage messageProcessorQueueMessage)
    {
        EventProcessorQueueMessage = eventProcessorQueueMessage;
        MessageProcessorQueueMessage = messageProcessorQueueMessage;
    }

    /// <summary>
    /// Message heading to the event processing queue.
    /// </summary>
    [QueueOutput("{source}-events-queue", Connection = nameof(IWorkerFunctionSettings.ApplicationStorage))]
    public BasicQueueMessage EventProcessorQueueMessage { get; set; }

    /// <summary>
    /// Message heading to the raw message processing queue.
    /// </summary>
    [QueueOutput("{source}-messages-queue", Connection = nameof(IWorkerFunctionSettings.ApplicationStorage))]
    public BasicQueueMessage MessageProcessorQueueMessage { get; set; }
}