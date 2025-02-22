using Business.Interfaces;

namespace Business.Models;

public abstract partial class Result : IResult
{
    public bool Success { get; protected set; }

    public int StatusCode { get; protected set; }

    public string? ErrorMessage { get; protected set; } 

    public static Result OK()
    {
        return new SuccessResult(200);
    }

    public static Result BadRequest(string message)
    {
        return new ErrorResult(400, message);
    }

    public static Result NotFound(string message)
    {
        return new ErrorResult(404, message);
    }

    public static Result AlreadyExists(string message)
    {
        return new ErrorResult(409, message);
    }

    public static Result Error(string message)
    {
        return new ErrorResult(400, message);
    }
}

public class Result<T> : Result
{
    public T? Data { get; private set; }

    public static Result<T> Ok(T? Data)
    {
        return new Result<T>
        {
            Success = true,
            StatusCode = 200,
            Data = Data
        };
    }

    public static Result<T> BadRequest(string message)
    {
        return new Result<T>
        {
            Success = false,
            StatusCode = 400,
            ErrorMessage = message
        };
    }

    public static Result<T> NotFound(string message)
    {
        return new Result<T>
        {
            Success = false,
            StatusCode = 404,
            ErrorMessage = message
        };
    }

    public static Result<T> AlreadyExists(string message)
    {
        return new Result<T>
        {
            Success = false,
            StatusCode = 409,
            ErrorMessage = message
        };
    }

    public static Result<T> Error(string message)
    {
        return new Result<T>
        {
            Success = false,
            StatusCode = 400,
            ErrorMessage = message
        };
    }
}

  

