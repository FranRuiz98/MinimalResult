using System;

namespace Result
{
    public static class Extensions
    {
        /// <summary>
        /// Matches over a non-generic <see cref="Result"/> producing a value of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type returned by the match functions.</typeparam>
        /// <param name="result">The result to match on.</param>
        /// <param name="onSuccess">Function invoked when the result is successful.</param>
        /// <param name="onFailure">Function invoked when the result is a failure.</param>
        /// <returns>The value returned by the invoked function.</returns>
        public static TResult Match<TResult>(this Result result, Func<TResult> onSuccess, Func<TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure();
        }

        /// <summary>
        /// Matches over a generic <see cref="Result{T}"/> producing a value of type <typeparamref name="TResult"/>.
        /// </summary>
        public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }

        /// <summary>
        /// Maps the value inside a successful <see cref="Result{T}"/> to a new value, preserving failure.
        /// </summary>
        public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> map)
        {
            return result.IsSuccess ? Result<TResult>.Success(map(result.Value)) : Result<TResult>.Failure(result.Error);
        }

        /// <summary>
        /// Binds a non-generic <see cref="Result"/>, invoking <paramref name="bind"/> when the
        /// original result is successful. If the original result is a failure, it is returned unchanged.
        /// </summary>
        public static Result Bind(this Result result, Func<Result> bind)
        {
            return result.IsSuccess ? bind() : result;
        }
    }
}
