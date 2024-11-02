using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;

namespace Nandun.Reference.WorkerFunction.Functions;

/// <summary>
/// Health check API to be used to verify if application is online and connectivity is established.
/// </summary>
public class HttpHealthFunction
{
    /// <summary>
    /// Gets an OK response to client can successfully reach the endpoint.
    /// </summary>
    /// <param name="req"></param>
    /// <returns>200 Response code</returns>
    [Function("http-health")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "health")] HttpRequestData req)
    {
        // HINT: Don't do anything other than returning an OK here. If additional/complex health checks are
        // required, please create a separate function. 
        return req.CreateResponse(HttpStatusCode.OK);
    }

}