using Domain.DTOs;
using MediscreenAPI.Controllers;
using MediscreenAPI.Model;
using MediscreenWepApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediscreenWebApp.tests.IntegrationTests
{
    public class PatientControllerTests
    {

        private Mock<IPatientService> _mockPatientService;

        public PatientControllerTests()
        {
            //Arrange
            _mockPatientService = new();
        }

        [Fact]
        public async Task TestIndexReturnViewResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.ReadAllPatients()).ReturnsAsync(() => new List<PatientDto>());

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Index();

            //assert
            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public async Task TestDetailsReturnViewResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.ReadPatient(It.IsAny<int>())).ReturnsAsync(() => new PatientDto());

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Details(1);

            //assert
            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public async Task TestDeleteReturnViewResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.ReadPatient(It.IsAny<int>())).ReturnsAsync(() => new PatientDto());

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Delete(1);

            //assert
            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public async Task TestAddPatientReturnRedirectToActionResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.CreatePatient(It.IsAny<PatientDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Add(new PatientDto
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "12 street hello",
                Phone = "01-12-33-33-33",
                Dob = new DateTime(2019, 05, 09, 9, 15, 0),
                Sex = Sex.M
            });

            Assert.IsType<RedirectToActionResult>(res);
        }

        [Fact]
        public async Task TestEditPatientReturnRedirectToActionResult()
        {
            //Arrange
            _mockPatientService.Setup(s => s.UpdatePatient(It.IsAny<PatientDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Edit(1, new PatientDto
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "12 street hello",
                Phone = "01-12-33-33-33",
                Dob = new DateTime(2019, 05, 09, 9, 15, 0),
                Sex = Sex.M
            });

            Assert.IsType<RedirectToActionResult>(res);
        }

        [Fact]
        public async Task TestEditWrongIdReturnNotFound()
        {
            //Arrange
            _mockPatientService.Setup(s => s.UpdatePatient(It.IsAny<PatientDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Edit(2, new PatientDto
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "12 street hello",
                Phone = "01-12-33-33-33",
                Dob = new DateTime(2019, 05, 09, 9, 15, 0),
                Sex = Sex.M
            });

            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async Task TestEditTransferNotFound()
        {
            //Arrange
            _mockPatientService.Setup(s => s.UpdatePatient(It.IsAny<PatientDto>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.NotFound));

            //act
            PatientController patientController = new(_mockPatientService.Object);
            IActionResult res = await patientController.Edit(1, new PatientDto
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "12 street hello",
                Phone = "01-12-33-33-33",
                Dob = new DateTime(2019, 05, 09, 9, 15, 0),
                Sex = Sex.M
            });

            Assert.IsType<StatusCodeResult>(res);
        }
    }
}
