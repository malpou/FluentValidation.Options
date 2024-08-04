using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MP.FluentValidation.Options
{
    internal static class OptionsBuilderFluentValidationExtensions
    {
        internal static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
                provider => new FluentValidationOptions<TOptions>(
                    optionsBuilder.Name, provider));
            return optionsBuilder;
        }
    }
}