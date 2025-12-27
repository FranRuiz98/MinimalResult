using System;

namespace Result
{
    public static class Extensions
    {
        public static TResult Match<TResult>(this Result result, Func<TResult> onSuccess, Func<TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure();
        }

        public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }

        public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> map)
        {
            return result.IsSuccess ? Result<TResult>.Success(map(result.Value)) : Result<TResult>.Failure(result.Error);
        }

        public static Result Bind(this Result result, Func<Result> bind)
        {
            return result.IsSuccess ? bind() : result;
        }
    }
}
