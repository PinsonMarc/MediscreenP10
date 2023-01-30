using FluentValidation;
using MediscreenAPI.Controllers;
using MediscreenAPI.Model.Entities;
using MediscreenWepApp.Services;
using Microsoft.AspNetCore.Mvc;
using PoseidonApi.Services;
using System.Net;

namespace MediscreenWebApp.tests.IntegrationTests
{
    public class PatHistoryControllerTests
    {

        private Mock<IPatientService> _mockPatientService;
        private IValidator<NoteDto> _validator;

        public PatHistoryControllerTests()
        {
            //Arrange
            _mockPatientService = new();
            _validator = new NoteValidator();
        }

        [Fact]
        public async Task TestIndexReturnViewResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.ReadHistory(It.IsAny<int>())).ReturnsAsync(() => new List<string>());

            //act
            PatHistoryController patHistoryController = new(_mockPatientService.Object, _validator);
            IActionResult res = await patHistoryController.Index(1);

            //assert
            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public async Task TestIndexNoIdReturnNotFoundResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.ReadHistory(It.IsAny<int>())).ReturnsAsync(() => new List<string>());

            //act
            PatHistoryController patHistoryController = new(_mockPatientService.Object, _validator);
            IActionResult res = await patHistoryController.Index(null);

            //assert
            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async Task TestAddReturnNoContent()
        {
            //Arrange
            _mockPatientService.Setup(s => s.AddNote(It.IsAny<NoteDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            PatHistoryController patHistoryController = new(_mockPatientService.Object, _validator);
            IActionResult res = await patHistoryController.Add(new NoteDto
            {
                PatId = 1,
                Note = "note"
            });

            //assert
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestAddEmptyReturnBadRequest()
        {
            //Arrange
            _mockPatientService.Setup(s => s.AddNote(It.IsAny<NoteDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            PatHistoryController patHistoryController = new(_mockPatientService.Object, _validator);
            IActionResult res = await patHistoryController.Add(new NoteDto());

            //assert
            Assert.IsType<BadRequestResult>(res);
        }
    }
}
