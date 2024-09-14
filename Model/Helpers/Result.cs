
namespace Model.Helpers
{
    public struct Result
    {
        public object Value { get; }
        public bool IsSuccessful { get; private set; }
        public bool IsFailure => !IsSuccessful;
        public string Message { get; private set; }

        private Result(object value, bool isSuccessful, string message)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public static Result Success(object value) => new Result(value, true, string.Empty);
        public static Result Failure(object  value, string errorMessage) => new Result(value, false, errorMessage);

        public Result Combine(params Result[] results)
        {
            var result = results.Where(r => r.IsFailure == true);
            if (result.Any())    
                IsSuccessful = false;
            else
                IsSuccessful = true;

            var combinedMessage = string.Join(Environment.NewLine, results.Select(r => r.Message));
            return new Result(Value, IsSuccessful, combinedMessage);
        }

        public void SetMessage(string text)
        {
            Message = text;
        }

        public void SetIsSuccessful(bool isSuccessful) => IsSuccessful = isSuccessful;
    }
}
