namespace Business.Models;

public abstract partial class Result
{
    public class ErrorResult : Result
    {
        public ErrorResult(int statusCode, string errorMessage)
        {
            Success = false;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;

        }
    }
}
