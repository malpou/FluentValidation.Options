# MP.FluentValidation.Options

`MP.FluentValidation.Options` is a NuGet package that provides an extension method to integrate `FluentValidation`
with `Microsoft.Extensions.Options`. It allows you to automatically validate configuration settings using
FluentValidation validators.

## Installation

To install the package, run the following command in your NuGet Package Manager Console

```bash
Install-Package MP.FluentValidation.Options
```

## How It Works

The `AddValidatedOptions` extension method:

1. Finds the `AbstractValidator` for the specified configuration class.
2. Validates the configuration values from `appsettings.json` or environment variables.
3. Registers the configuration class with the validated values in the `IOptions<T>` service.

## Usage

### Step 1: Define Your Configuration Class

Create a configuration class that represents the settings you want to validate.

```csharp
public class MyConfiguration
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Step 2: Define Your Validator

Create a validator class that inherits from `AbstractValidator<T>`, where `T` is the configuration class.

```csharp
public class MyConfigurationValidator : AbstractValidator<MyConfiguration>
{
    public MyConfigurationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Age).GreaterThan(0);
    }
}
```

### Step 3: Register the Configuration and Validator to the Service Collection

Use the `AddValidatedOptions` extension method to register your configuration class and its validator.

```csharp
services.AddValidatedOptions<MyConfiguration>();
```

OBS: The `AddValidatedOptions` will by default use the `MyConfiguration` class as the section name for the configuration
settings. If you want to use a different key, you can pass it as a parameter to the method like this:

```csharp
services.AddValidatedOptions<MyConfiguration>(sectionName: "MySettings");
```

### Step 4: Use the Configuration in Your Application

Inject `IOptions<MyConfiguration>` into your classes to use the validated configuration settings.

```csharp
public class MyService
{
    private readonly MyConfiguration _configuration;

    public MyService(IOptions<MyConfiguration> options)
    {
        _configuration = options.Value;
    }

    public void DoSomething()
    {
        // Use the configuration settings
        Console.WriteLine($"Name: {_configuration.Name}");
        Console.WriteLine($"Age: {_configuration.Age}");
    }
}
```

## License

This project is licensed under the MIT License. See the LICENSE file for more details.