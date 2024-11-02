using System.Net;
using System.Text;
using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Moq;
using Azure.Storage.Blobs;
using Nandun.Reference.WorkerFunction.Bindings;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nandun.Reference.WorkerFunction.Mock;

namespace Nandun.Reference.WorkerFunction.Functions;

[TestClass]
public class HttpGenericDataFunctionTests
{

    private const string TEST_CONNECTION_STRING = "UseDevelopmentStorage=true";
    private const string TEST_SOURCE = "Olo";
    private const string TEST_CONTAINER = "raw";
    private const string TEST_BLOB = "Guid";
    private HttpGenericDataFunction _subject;

    private Mock<FunctionContext> _contextMock;
    private Mock<ILogger<HttpGenericDataFunction>> _loggerMock;
    private Mock<HttpRequestData> _reqMock;
    private Mock<HttpResponseData> _resMock;
    private Mock<BlobClient> _blobMock;

    [TestInitialize]
    public void Setup()
    {
        _contextMock = new Mock<FunctionContext>();
        _loggerMock = new Mock<ILogger<HttpGenericDataFunction>>();
        _reqMock = new Mock<HttpRequestData>(() => new MockHttpRequestData(_contextMock.Object));
        _resMock = new Mock<HttpResponseData>(() => new MockHttpResponseData(_contextMock.Object));
        _blobMock = new Mock<BlobClient>(() => new BlobClient(TEST_CONNECTION_STRING, TEST_CONTAINER, TEST_BLOB));

        _subject = new HttpGenericDataFunction(_loggerMock.Object);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public async Task HttpGenericDataFunction_Run_WhenInvoked_ReturnsMessagesToBothOutputBindings()
    {
        _blobMock.Setup(m => m.OpenWriteAsync(It.IsAny<bool>(), default, default)).ReturnsAsync(new MemoryStream());
        _blobMock.Setup(m => m.Name).Returns(TEST_BLOB);


        _reqMock.Setup(m => m.Headers).Returns(() =>
            new HttpHeadersCollection(
                new KeyValuePair<string, IEnumerable<string>>[]
                {
                    new("X-TrackingKey", new[] {"Guid"}),
                    new("X-Olo-Event-Type", new[] {"Guid"}),
                    new("X-Olo-Timestamp", new[] {DateTimeOffset.Now.Ticks.ToString()})
                }));
        _reqMock.Setup(m => m.Body).Returns(new MemoryStream(Encoding.UTF8.GetBytes("Test Body")));
        _reqMock.Setup(m => m.CreateResponse()).Returns(_resMock.Object);

        _resMock.Setup(m => m.Body).Returns(new MemoryStream());
        _resMock.Setup(m => m.Headers).Returns(new HttpHeadersCollection());
        _resMock.SetupSet(m => m.StatusCode = It.IsAny<HttpStatusCode>())
            .Callback(new Action<HttpStatusCode>(code => Assert.AreEqual(HttpStatusCode.Accepted, code)));

        GenericDataOutput output = await _subject.RunAsync(_reqMock.Object, _blobMock.Object, TEST_SOURCE);

        Assert.IsNotNull(output);
        Assert.IsNotNull(output.EventProcessorQueueMessage);
        Assert.IsNotNull(output.MessageProcessorQueueMessage);
    }

    [TestMethod]
    [TestCategory("Unit")]
    public async Task HttpGenericDataFunction_Run_WhenSourceContainsSpaces_ReturnsBadRequest()
    {
        _reqMock.Setup(m => m.CreateResponse()).Returns(_resMock.Object);

        Mock<IServiceProvider> serviceProviderMock = new();
        Mock<IOptions<WorkerOptions>> optionsMock = new();
        WorkerOptions workerOptions = new ()
        {
            Serializer = new JsonObjectSerializer(new JsonSerializerOptions())
        };

        optionsMock.Setup(m => m.Value).Returns(workerOptions);

        serviceProviderMock.Setup(m => m.GetService(typeof(IOptions<WorkerOptions>))).Returns(optionsMock.Object);

        _contextMock.Setup(m => m.InstanceServices).Returns(serviceProviderMock.Object);

        _resMock.Setup(m => m.Body).Returns(new MemoryStream());
        _resMock.Setup(m => m.Headers).Returns(new HttpHeadersCollection());
        _resMock.SetupSet(m => m.StatusCode = It.IsAny<HttpStatusCode>())
            .Callback(new Action<HttpStatusCode>(code => Assert.AreEqual(HttpStatusCode.BadRequest, code)));

        GenericDataOutput output = await _subject.RunAsync(_reqMock.Object, _blobMock.Object, "Sauce with a space");

        Assert.IsNull(output);
    }
}