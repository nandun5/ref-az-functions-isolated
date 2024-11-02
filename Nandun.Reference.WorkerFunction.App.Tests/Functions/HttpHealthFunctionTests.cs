using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Nandun.Reference.WorkerFunction.Mock;

namespace Nandun.Reference.WorkerFunction.Functions;

[TestClass]
public class HttpHealthFunctionTests
{
    private Mock<FunctionContext> _contextMock;
    private Mock<HttpResponseData> _resMock;
    private Mock<HttpRequestData> _reqMock;
    private HttpHealthFunction _subject;

    [TestInitialize]
    public void Setup()
    {
        _contextMock = new Mock<FunctionContext>();
        _resMock = new Mock<HttpResponseData>(() => new MockHttpResponseData(_contextMock.Object));
        _reqMock = new Mock<HttpRequestData>(() => new MockHttpRequestData(_contextMock.Object));

        _subject = new HttpHealthFunction();
    }

    [TestMethod]
    [TestCategory("Unit")]
    public void HttpHealthFunction_Run_WhenInvoked_ReturnsOKResponse()
    {
        _reqMock.Setup(m => m.CreateResponse()).Returns(_resMock.Object);

        _resMock.SetupSet(setterExpression: m => m.StatusCode = It.IsAny<HttpStatusCode>())
            .Callback(new Action<HttpStatusCode>(value => Assert.AreEqual(HttpStatusCode.OK, value)));

        _ = _subject.Run(_reqMock.Object);
    }
}
