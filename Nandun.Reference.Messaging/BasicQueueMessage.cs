using System.Diagnostics.CodeAnalysis;

namespace Nandun.Reference.WorkerFunction;

/// <summary>
/// Basic Queue Message, where payload specific information is not known, however contains basic
/// metadata on the message.
/// </summary>
[ExcludeFromCodeCoverage]
public record BasicQueueMessage
{
    /// <summary>
    /// Default constructor initialization.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="trackingKey"></param>
    /// <param name="asOf"></param>
    /// <param name="headers"></param>
    /// <param name="dataPointer"></param>
    public BasicQueueMessage(string source, string trackingKey, DateTimeOffset asOf,
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string dataPointer)
    {
        Source = source;
        TrackingKey = trackingKey;
        AsOf = asOf;
        Headers = headers;
        DataPointer = dataPointer;
    }

    /// <summary>
    /// Source of this message
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Tracking key associated with this message
    /// </summary>
    public string TrackingKey { get; set; }

    /// <summary>
    /// Originating time stamp
    /// </summary>
    public DateTimeOffset AsOf { get; set; }

    /// <summary>
    /// All Http Headers from the source message
    /// </summary>
    public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }

    /// <summary>
    /// Blob name containing the data of this message
    /// </summary>
    public string DataPointer { get; set; }
}
