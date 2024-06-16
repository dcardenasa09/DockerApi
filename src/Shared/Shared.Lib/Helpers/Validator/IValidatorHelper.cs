namespace Shared.Lib.Helpers.Validator;

public interface IValidatorHelper<TDTO> {
    Task Validate(TDTO tDto);
}