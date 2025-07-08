using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    /// <summary>
    /// Infrastructure test for validating CreateVehicleRequest model binding and validation.
    /// </summary>
    [Collection(TestCollections.TestServer)]
    public sealed class CreateVehicleRequestValidationTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        /// <summary>
        /// Verifies that sending an invalid CreateVehicleRequest returns 400 Bad Request.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task PostCreateVehicleReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            using var client = Fixture.Server.CreateClient();
            client.DefaultRequestVersion = HttpVersion.Version11;

            var invalidRequest = new
            {
                // Missing required fields (LicensePlate)
                brand = "Audi",
                model = "A3",
                manufactureDate = "2022-01-01"
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(invalidRequest),
                Encoding.UTF8,
                "application/json");

            var uri = new Uri("/vehicles", UriKind.Relative);

            // Act
            var response = await client.PostAsync(uri, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
