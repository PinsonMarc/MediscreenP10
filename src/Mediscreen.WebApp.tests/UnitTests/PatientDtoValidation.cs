using Domain.DTOs;
using FluentValidation.TestHelper;
using MediscreenAPI.Model;
using Moq;
using PoseidonApi.Services;

namespace MediscreenWebApp.tests.UnitTests
{
    public class PatientDtoValidation
    {
        private PatientDto _dto;
        private PatientValidator _validator;

        public PatientDtoValidation()
        {
            //Arrange
            _dto = Mock.Of<PatientDto>();
            _validator = new PatientValidator();
        }

        [Fact]
        public void ValidateFullNoError()
        {
            _dto.Family = "Family";
            _dto.Given = "Given";
            _dto.Address = "12 street hello";
            _dto.Phone = "01-12-33-33-33";
            _dto.Dob = new DateTime(2019, 05, 09, 9, 15, 0);
            _dto.Sex = Sex.M;

            TestValidationResult<PatientDto> result = _validator.TestValidate(_dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidateMinInfoNoError()
        {
            _dto.Family = "Family";
            _dto.Given = "Given";
            _dto.Dob = new DateTime(2019, 05, 09, 9, 15, 0);
            _dto.Sex = Sex.M;

            TestValidationResult<PatientDto> result = _validator.TestValidate(_dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidateEmptyNameError()
        {
            _dto.Dob = new DateTime(2019, 05, 09, 9, 15, 0);
            _dto.Sex = Sex.M;

            TestValidationResult<PatientDto> result = _validator.TestValidate(_dto);

            result.ShouldHaveValidationErrorFor(x => x.Family).WithErrorCode("NotEmptyValidator");
            result.ShouldHaveValidationErrorFor(x => x.Given).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ValidateTooOldDobError()
        {
            _dto.Family = "Family";
            _dto.Given = "Given";
            _dto.Dob = new DateTime(1819, 05, 09, 9, 15, 0);
            _dto.Sex = Sex.M;

            TestValidationResult<PatientDto> result = _validator.TestValidate(_dto);

            result.ShouldHaveValidationErrorFor(x => x.Dob).WithErrorMessage("Date must be before the current date and after 01/01/1900.");
        }

        [Fact]
        public void ValidateAfterCurrentDateDobError()
        {
            _dto.Family = "Family";
            _dto.Given = "Given";
            _dto.Dob = new DateTime(2219, 05, 09, 9, 15, 0);
            _dto.Sex = Sex.M;

            TestValidationResult<PatientDto> result = _validator.TestValidate(_dto);

            result.ShouldHaveValidationErrorFor(x => x.Dob).WithErrorMessage("Date must be before the current date and after 01/01/1900.");
        }

    }
}