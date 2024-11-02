using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Net;

namespace Nandun.Reference.WorkerFunction.Mock;


/// <summary>
/// Use this class with the Moq framework to mock the usage of <see cref="HttpRequestData"/> for Mocking.
/// all members of this class are meant to be setup in Moq before use.
/// </summary>
/// <remarks>
/// var reqMock = new Mock&lt;HttpRequestData&gt;(() =&gt; new MockHttpRequestData(_contextMock.Object));
/// </remarks>
[ExcludeFromCodeCoverage]
public class MockHttpRequestData : HttpRequestData
{
    /// <inheritdoc />
    public MockHttpRequestData(FunctionContext functionContext) : base(functionContext)
    {
    }

    /// <inheritdoc />
    public override HttpResponseData CreateResponse() => null;

    /// <inheritdoc />
    public override Stream Body => null;

    /// <inheritdoc />
    public override HttpHeadersCollection Headers => null;

    /// <inheritdoc />
    public override IReadOnlyCollection<IHttpCookie> Cookies => null;

    /// <inheritdoc />
    public override Uri Url => null;

    /// <inheritdoc />
    public override IEnumerable<ClaimsIdentity> Identities => null;

    /// <inheritdoc />
    public override string Method => null;
}

/// <summary>
/// <see cref="HttpResponseData"/> for mocking with Moq.
/// </summary>
/// <remarks>
/// Example:
/// var mock = new Mock&lt;HttpResponseData&gt;(() =&gt; new MockHttpResponseData(_contextMock.Object));
/// _resMock.SetupSet(setterExpression: m =&gt; m.StatusCode = It.IsAny&lt;HttpStatusCode&gt;())
///     .Callback(new Action&lt;HttpStatusCode&gt;(value =&gt; Assert.AreEqual(HttpStatusCode.OK, value)));
/// </remarks>
[ExcludeFromCodeCoverage]
public class MockHttpResponseData : HttpResponseData
{
    /// <inheritdoc />
    public MockHttpResponseData(FunctionContext functionContext) : base(functionContext)
    {
    }

    /// <inheritdoc />
    public override HttpStatusCode StatusCode { get; set; }

    /// <inheritdoc />
    public override HttpHeadersCollection Headers { get; set; }

    /// <inheritdoc />
    public override Stream Body { get; set; }

    /// <inheritdoc />
    public override HttpCookies Cookies => null;
}