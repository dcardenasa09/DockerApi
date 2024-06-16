using FluentValidation.Results;

namespace Shared.Lib.Exceptions;

public class ValidationException: Exception {

    public Dictionary<string, string[]> Errors { get; }

    public ValidationException() : base("ValidationFailure") {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this() {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}