using Domain.DTOs;
using FluentValidation;

namespace PoseidonApi.Services
{
    public static class Validators
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PatientDto>, PatientValidator>();
        }
    }

    public class PatientValidator : AbstractValidator<PatientDto>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Given).NotEmpty();
            RuleFor(x => x.Family).NotEmpty();
            RuleFor(x => x.Sex).NotEmpty();
            RuleFor(x => x.Dob)
                .Must(x => x < DateOnly.FromDateTime(DateTime.Now) && x > DateOnly.FromDateTime(new DateTime(1900, 1, 1)))
                .WithMessage("Date must be before the current date and after 01/01/1900.");
        }
    }
}
