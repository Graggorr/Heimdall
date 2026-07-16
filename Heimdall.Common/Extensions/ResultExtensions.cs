using FluentResults;

namespace Heimdall.Common.Extensions;

public static class ResultExtensions
{
    extension(Result result)
    {
        public static Result WithRootError(Enum errorCode) => new Result().WithError(errorCode.ToString());
        
        public string ToStringError() => string.Join(',', result.Errors.SelectMany(error => error.Message));
    }
}
