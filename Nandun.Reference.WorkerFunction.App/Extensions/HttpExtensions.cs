using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Nandun.Reference.WorkerFunction.Extensions;

/// <summary>
/// Provides a Web API-ish action result helper methods for the functions with http trigger.
/// </summary>
public static class HttpExtensions
{
    /// <summary> Creates a response with <see cref="HttpStatusCode.OK"/>. </summary>
    /// <param name="req"> The request associated with the response. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData Ok(this HttpRequestData req) => CreateResponse(req, HttpStatusCode.OK);

    /// <summary> Creates a response with <see cref="HttpStatusCode.OK"/>. </summary>
    /// <typeparam name="T"> The type of the payload. </typeparam>
    /// <param name="req"> The request associated with the response. </param>
    /// <param name="payload"> The payload to return. Optional. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData Ok<T>(this HttpRequestData req, T payload)
        => CreateTypedResponse<T>(req, HttpStatusCode.OK, payload);

    /// <summary> Creates a response with <see cref="HttpStatusCode.Created"/>. </summary>
    /// <param name="req"> The request associated with the response. </param>
    /// <param name="url"> The endpoint to locate the created resource. Optional. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData Created(this HttpRequestData req, string? url = default)
    {
        HttpResponseData res = CreateResponse(req, HttpStatusCode.Created);
        _ = !string.IsNullOrWhiteSpace(url) ? res.Headers.TryAddWithoutValidation("Location", url) : default;

        return res;
    }

    /// <summary> Creates a response with <see cref="HttpStatusCode.Accepted"/>. </summary>
    /// <param name="req"> The request associated with the response. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData Accepted(this HttpRequestData req)
        => CreateResponse(req, HttpStatusCode.Accepted);

    /// <summary> Creates a response with <see cref="HttpStatusCode.BadRequest"/>. </summary>
    /// <param name="req"> The request associated with the response. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData BadRequest(this HttpRequestData req)
        => CreateResponse(req, HttpStatusCode.BadRequest);

    /// <summary> Creates a response with <see cref="HttpStatusCode.BadRequest"/>. </summary>
    /// <typeparam name="T"> The type of the payload. </typeparam>
    /// <param name="req"> The request associated with the response. </param>
    /// <param name="payload"> The specific value as response body. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData BadRequest<T>(this HttpRequestData req, T payload)
        => CreateTypedResponse<T>(req, HttpStatusCode.BadRequest, payload);

    /// <summary> Creates a response with <see cref="HttpStatusCode.InternalServerError"/>. </summary>
    /// <param name="req"> The request associated with the response. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData InternalServerError(this HttpRequestData req)
        => CreateResponse(req, HttpStatusCode.InternalServerError);

    /// <summary> Creates a response with <see cref="HttpStatusCode.InternalServerError"/>. </summary>
    /// <typeparam name="T"> The type of the payload. </typeparam>
    /// <param name="req"> The request associated with the response. </param>
    /// <param name="payload"> The specific value as response body. </param>
    /// <returns> The response. </returns>
    public static HttpResponseData InternalServerError<T>(this HttpRequestData req, T payload)
        => CreateTypedResponse<T>(req, HttpStatusCode.InternalServerError, payload);

    internal static HttpResponseData CreateResponse(HttpRequestData req, HttpStatusCode status)
        => req.CreateResponse(status);

    internal static HttpResponseData CreateTypedResponse<T>(this HttpRequestData req, HttpStatusCode status, T payload)
    {
        HttpResponseData res = req.CreateResponse();

        // BUG: https://github.com/Azure/azure-functions-dotnet-worker/issues/344
        //res.StatusCode = status;
        res.WriteAsJsonAsync<T>(payload, status);

        return res;
    }

    /// <summary>
    /// Adds a header key and value pair to an <see cref="HttpRequestData"/>
    /// </summary>
    /// <returns><see cref="HttpRequestData"/> for fluent builder style method addition</returns>
    public static HttpRequestData AddHeader(this HttpRequestData req, string name, string value)
    {
        req.Headers.Add(name, value);
        return req;
    }

    /// <summary>
    /// Adds a header key and value pair to an <see cref="HttpResponseData"/>
    /// </summary>
    /// <returns><see cref="HttpResponseData"/> for fluent builder style method addition</returns>
    public static HttpResponseData AddHeader(this HttpResponseData res, string name, string value)
    {
        res.Headers.Add(name, value);
        return res;
    }
}
