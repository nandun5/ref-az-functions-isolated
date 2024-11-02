namespace Nandun.Reference.WorkerFunction.Settings;

/// <summary>
/// Application settings for the WorkerFunction. Use this to inject
/// and access the application settings from the services.
/// </summary>
internal interface IWorkerFunctionSettings
{
    /// <summary>
    /// Application Storage Account connection string
    /// </summary>
    string ApplicationStorage { get; }

    /// <summary>
    /// Blob container for Raw data
    /// </summary>
    string RawDataContainerName { get; }
}