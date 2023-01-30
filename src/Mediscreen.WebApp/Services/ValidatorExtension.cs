using Domain.DTOs;
using FluentValidation;
using MediscreenAPI.Model.Entities;

namespace PoseidonApi.Services
{
    public static class Validators
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PatientDto>, PatientValidator>();
            services.AddScoped<IValidator<NoteDto>, NoteValidator>();
        }
    }

    public class PatientValidator : AbstractValidator<PatientDto>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Given).NotEmpty();
            RuleFor(x => x.Family).NotEmpty();
            RuleFor(x => x.Sex).NotNull();
            RuleFor(x => x.Dob)
                .Must(x => x < DateTime.Now && x > new DateTime(1900, 1, 1))
                .WithMessage("Date must be before the current date and after 01/01/1900.");
        }
    }

    public class NoteValidator : AbstractValidator<NoteDto>
    {
        public NoteValidator()
        {
            RuleFor(x => x.Note).NotEmpty();
            RuleFor(x => x.PatId).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
