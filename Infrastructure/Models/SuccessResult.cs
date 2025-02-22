namespace Business.Models;

public  abstract partial class Result
{
    public class SuccessResult : Result
    {
        public SuccessResult(int statusCode)
        {
            Success = true;
            StatusCode = statusCode;

        }
    }
}
