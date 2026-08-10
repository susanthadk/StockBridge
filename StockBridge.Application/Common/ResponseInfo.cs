using System.Net;

namespace StockBridge.Application.Common;

public class ResponseInfo<T>
{
    public bool IsSuccess { get; set; }
    public string InfoMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ResponseInfo<T> Success(T? data, HttpStatusCode httpStatusCode, string message)
    {
        return new ResponseInfo<T>
        {
            IsSuccess = true,
            InfoMessage = message,
            Data = data,
            StatusCode = httpStatusCode
        };
    }

    public static ResponseInfo<T> Failure(string message, HttpStatusCode httpStatusCode)
    {
        return new ResponseInfo<T>
        {
            IsSuccess = false,
            InfoMessage = message,
            StatusCode = httpStatusCode
        };
    }
}