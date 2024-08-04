namespace FluentValidation.Options.Tests;

public partial class ValidatedOptionsExtensionsTests
{
    private class TestOptionsNestedValidator
    {
        public class Validator : AbstractValidator<TestOptionsNestedValidator>
        {
            public Validator()
            {
                RuleFor(x => x).NotNull();
            }
        }
    }

    private class TestOptionsExternalValidator
    {
    }


    private class TestOptionsExternalValidatorValidator : AbstractValidator<TestOptionsExternalValidator>
    {
        public TestOptionsExternalValidatorValidator()
        {
            RuleFor(x => x).NotNull();
        }
    }

    private class TestOptionsNoValidator
    {
    }
}