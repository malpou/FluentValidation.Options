using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MP.FluentValidation.Options.Tests;

public partial class ValidatedOptionsExtensionsTests
{
    [Fact]
    public void AddValidatedOptions_ShouldRegisterValidator_WhenValidatorIsNested()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddValidatedOptions<TestOptionsNestedValidator>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var validator = serviceProvider.GetService<IValidator<TestOptionsNestedValidator>>();
        Assert.NotNull(validator);
        Assert.IsType<TestOptionsNestedValidator.Validator>(validator);
    }

    [Fact]
    public void AddValidatedOptions_ShouldRegisterValidator_WhenValidatorIsExternal()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddValidatedOptions<TestOptionsExternalValidator>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var validator = serviceProvider.GetService<IValidator<TestOptionsExternalValidator>>();
        Assert.NotNull(validator);
        Assert.IsType<TestOptionsExternalValidatorValidator>(validator);
    }

    [Fact]
    public void AddValidatedOptions_ShouldThrowException_WhenValidatorDoesNotExist()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert
        var exception =
            Assert.Throws<InvalidOperationException>(() => services.AddValidatedOptions<TestOptionsNoValidator>());
        Assert.Equal($"Validator class not found for {nameof(TestOptionsNoValidator)}", exception.Message);
    }
}