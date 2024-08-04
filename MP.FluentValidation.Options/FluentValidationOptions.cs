using System;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MP.FluentValidation.Options
{
    internal class FluentValidationOptions<TOptions> : IValidateOptions<TOptions>
        where TOptions : class
    {
        private readonly string _name;
        private readonly IServiceProvider _serviceProvider;

        internal FluentValidationOptions(string name, IServiceProvider serviceProvider)
        {
            _name = name;
            _serviceProvider = serviceProvider;
        }

        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            if (_name != null && _name != name)
                return ValidateOptionsResult.Skip;

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            using (var scope = _serviceProvider.CreateScope())
            {
                var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

                var results = validator.Validate(options);
                if (results.IsValid) return ValidateOptionsResult.Success;

                var typeName = options.GetType().Name;
                var errors = results.Errors.Select(result =>
                        $"Validation failed for '{typeName}.{result.PropertyName}' with the error: '{result.ErrorMessage}'.")
                    .ToList();

                return ValidateOptionsResult.Fail(errors);
            }
        }
    }
}