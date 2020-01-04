using System;

namespace PersonalFinance.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        protected Result(bool isSuccess) => IsSuccess = isSuccess;
        
        protected Result(bool isSuccess, string error) 
            : this(isSuccess)
        {
            if(!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException();

            Error = error;
        }
        
        public static Result Ok() => new Result(true);
        
        public static Result Fail(string message) => new Result(false, message);
        
        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);

        public static Result<T> Fail<T>(string message) => new Result<T>(default, false, message);
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if(!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error) 
            => _value = value;
    }
}