using Domain.DTOs;
using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using MediscreenAPI.tests.FunctionalTests;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace PoseidonApi.Tests.IntegrationTests
{
    public class RoutingTest : FunctionalTests
    {
        [Theory]
        [InlineData("/Assess/ById/999")]
        [InlineData("/Assess/ByFamilyName/NoOne")]
        [InlineData("/Patients/Read/9999")]
        [InlineData("/PatHistory/Read/9999")]
        public async Task GetNotExistingReturnNotFound(string url)
        {
            HttpResponseMessage response = await TestClient.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Theory]
        [InlineData("/Patients/Read")]
        public async Task GetReturnSuccess(string url)
        {
            HttpResponseMessage response = await TestClient.GetAsync(url);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString().Trim());
        }

        [Fact]
        public async Task GetSwaggerReturnSuccess()
        {
            // Act
            HttpResponseMessage response = await TestClient.GetAsync("/swagger");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        [Fact]
        public async Task CreatePatientReturnSuccess()
        {
            PatientDto dto = new()
            {
                Id = 100,
                Family = "Family",
                Given = "Given",
                Address = "12 street hello",
                Phone = "01-12-33-33-33",
                Dob = new DateTime(2019, 05, 09, 9, 15, 0),
                Sex = Sex.M
            };

            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await TestClient.PostAsync("/Patients/Create", content);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }


        [Fact]
        public async Task CreatePatientNotesReturnSuccess()
        {
            NoteDto dto = new()
            {
                PatId = 1,
                Note = "Test Note"
            };

            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await TestClient.PostAsync("/PatHistory/Add", content);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}