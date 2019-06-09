namespace Sample.Rekognition.MediatR
{
    public class Result
    {
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
