using FluentResults;

namespace Heimdall.Abstractions.Extensions;

public static class ResultExtensions
{
    extension(Result result)
    {
        public static Result WithRootError(Enum errorCode) => new Result().WithError(errorCode.ToString());

        public string ToStringError() => string.Join(',', result.Errors.Select(error => error.Message));
    }
}
