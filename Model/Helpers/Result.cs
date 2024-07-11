
namespace Model.Helpers
{
    public struct Result<T>
    {
        public T Value { get; }
        public bool IsSuccessful { get; private set; }
        public bool IsFailure => !IsSuccessful;
        public string Message { get; private set; }

        private Result(T value, bool isSuccessful, string message)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);
        public static Result<T> Failure(T value, string errormessage) => new Result<T>(value, false, errormessage);

        public Result<T> Combine(params Result<T>[] results)
        {
            var result = results.Where(r => r.IsFailure == true);
            if (result.Any())    
                IsSuccessful = false;
            else
                IsSuccessful = true;

            var combinedMessage = string.Join(Environment.NewLine, results.Select(r => r.Message));
            return new Result<T>(Value, IsSuccessful, combinedMessage);
        }

        public void SetMessage(string text)
        {
            Message = text;
        }

        public void SetIsSuccessful(bool isSuccessful) => IsSuccessful = isSuccessful;
    }
}
