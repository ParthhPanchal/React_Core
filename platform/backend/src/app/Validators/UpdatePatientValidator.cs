using FluentValidation;
using Hospital.Application.DTOs;
using System;

namespace Hospital.Application.Validators
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Patient ID is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past")
                .GreaterThan(DateTime.Today.AddYears(-150)).WithMessage("Invalid date of birth");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be Male, Female, or Other");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters")
                .Matches(@"^[\d\s\-\+\(\)]+$").WithMessage("Invalid phone number format");

            RuleFor(x => x.BloodGroup)
                .Must(bg => string.IsNullOrEmpty(bg) || 
                    new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" }.Contains(bg))
                .WithMessage("Invalid blood group");

            RuleFor(x => x.ZipCode)
                .MaximumLength(10).WithMessage("Zip code cannot exceed 10 characters")
                .When(x => !string.IsNullOrEmpty(x.ZipCode));
        }
    }
}

