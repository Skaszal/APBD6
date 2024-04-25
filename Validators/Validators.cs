namespace APBD6.Validators;
using FluentValidation;

public static class Validators
{
    public static void RegisterValidators(this IServiceCollection services)
    {
       services.AddValidatorsFromAssemblyContaining<AnimalCreateRequestValidator>();
       services.AddValidatorsFromAssemblyContaining<AnimalReplaceRequestValidator>();
    }
}