using System.Diagnostics.CodeAnalysis;

namespace Nandun.Reference.WorkerFunction;

/// <summary>
/// Event Queue Message contains all the information of a <see cref="BasicQueueMessage"/>,
/// as well as properties of an Event 
/// </summary>
[ExcludeFromCodeCoverage]
public record EventQueueMessage : BasicQueueMessage
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public EventQueueMessage(string source, string trackingKey, DateTimeOffset asOf,
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string dataPointer, string eventType,
        string fileName) : base(source, trackingKey, asOf, headers, dataPointer)
    {
        EventType = eventType;
        FileName = fileName;
    }

    /// <summary>
    /// Event type
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// File Name
    /// </summary>
    public string FileName { get; set; }

}