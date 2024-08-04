using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MP.FluentValidation.Options
{
    public static class ValidatedOptionsExtensions
    {
        public static void AddValidatedOptions<TOptions>(this IServiceCollection services, string sectionName = null)
            where TOptions : class
        {
            if (sectionName == null) sectionName = typeof(TOptions).Name;

            var validatorType = Assembly.GetAssembly(typeof(TOptions))
                .GetTypes()
                .FirstOrDefault(t => typeof(AbstractValidator<TOptions>).IsAssignableFrom(t) && !t.IsAbstract);

            if (validatorType == null)
                throw new InvalidOperationException($"Validator class not found for {typeof(TOptions).Name}");

            services.AddScoped(typeof(IValidator<TOptions>), validatorType);
            services.AddOptions<TOptions>()
                .BindConfiguration(sectionName)
                .ValidateFluentValidation()
                .ValidateOnStart();
        }
    }
}