namespace Nandun.Reference.WorkerFunction.Settings;

/// <inheritdoc/>
internal class WorkerFunctionSettings : IWorkerFunctionSettings
{

    public WorkerFunctionSettings()
    {
        ApplicationStorage = "Storage Name";
        RawDataContainerName = "Container";
    }

    public string ApplicationStorage { get; }

    public string RawDataContainerName { get; }
}