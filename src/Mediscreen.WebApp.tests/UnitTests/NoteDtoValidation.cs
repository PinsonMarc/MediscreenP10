using FluentValidation.TestHelper;
using MediscreenAPI.Model.Entities;
using PoseidonApi.Services;

namespace MediscreenWebApp.tests.UnitTests
{
    public class NoteDtoValidation
    {
        private NoteDto _dto;
        private NoteValidator _validator;

        public NoteDtoValidation()
        {
            //Arrange
            _dto = Mock.Of<NoteDto>();
            _validator = new NoteValidator();
        }

        [Fact]
        public void ValidateFullNoError()
        {
            _dto.Note = "Custom note";
            _dto.PatId = 1;

            TestValidationResult<NoteDto> result = _validator.TestValidate(_dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidateEmptyError()
        {
            TestValidationResult<NoteDto> result = _validator.TestValidate(_dto);

            result.ShouldHaveValidationErrorFor(x => x.Note).WithErrorCode("NotEmptyValidator");
            result.ShouldHaveValidationErrorFor(x => x.PatId).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ValidateWrongIdError()
        {
            _dto.Note = "Custom note";
            _dto.PatId = -1;

            TestValidationResult<NoteDto> result = _validator.TestValidate(_dto);

            result.ShouldHaveValidationErrorFor(x => x.PatId).WithErrorCode("GreaterThanOrEqualValidator");
        }
    }
}