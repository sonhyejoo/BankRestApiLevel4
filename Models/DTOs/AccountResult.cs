using System.Net;

namespace BankRestApi.Models.DTOs;

public class AccountResult<T> where T : class
{
    public Error? Error { get; }
    public T? Result { get; }
    public bool IsSuccess => Error is null;

    protected AccountResult(T? result, Error? error)
    {
        Result = result;
        Error = error;
    }

    public static AccountResult<T> Success(T result) => new(result, null);
    public static AccountResult<T> Failure(Error error) => new(null, error);
}