using Moq;

namespace Nandun.Reference.WorkerFunction.Settings;

[TestClass]
public class WorkerFunctionSettingsTests
{

    [TestInitialize]
    public void Setup()
    {
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void WorkerFunctionSettings_Get_WhenPropertyCalled_UnderlyingAppSettingsProviderInvoked()
    {
        WorkerFunctionSettings settings = new();


        _ = settings.ApplicationStorage;
        _ = settings.RawDataContainerName;
    }
}
