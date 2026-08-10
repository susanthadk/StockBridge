using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using System.Net;

namespace StockBridge.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected virtual ActionResult HandleResponse<T>(ResponseInfo<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(result),
            HttpStatusCode.Created => Created(string.Empty, result),
            HttpStatusCode.NoContent => Ok(result),
            HttpStatusCode.NotFound => NotFound(result),
            HttpStatusCode.BadRequest => BadRequest(result),
            HttpStatusCode.Unauthorized => Unauthorized(result),
            HttpStatusCode.InternalServerError => StatusCode(500, result),
            _ => StatusCode((int)result.StatusCode, result)
        };
    }
}