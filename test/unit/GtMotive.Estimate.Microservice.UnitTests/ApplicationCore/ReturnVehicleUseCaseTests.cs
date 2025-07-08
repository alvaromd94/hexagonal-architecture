using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.DomainModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    /// <summary>
    /// Unit tests for the ReturnVehicleUseCase.
    /// </summary>
    public class ReturnVehicleUseCaseTests
    {
        /// <summary>
        /// Ensures a vehicle is correctly returned when conditions are met.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task ShouldReturnVehicleSuccessfully()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();

            var vehicle = new Vehicle
            {
                Id = vehicleId,
                LicensePlate = "1234ABC",
                Brand = "Renault",
                Model = "Clio",
                IsRented = true,
                RentedByCustomerId = "Customer1"
            };

            var input = new ReturnVehicleInput(vehicleId);

            var repoMock = new Mock<IVehicleRepository>();
            repoMock.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var outputPortMock = new Mock<IOutputPortStandard<ReturnVehicleOutput>>();
            var outputInvalidMock = new Mock<IOutputPortInvalid>();

            var useCase = new ReturnVehicleUseCase(
                repoMock.Object,
                unitOfWorkMock.Object,
                outputPortMock.Object,
                outputInvalidMock.Object);

            // Act
            await useCase.ExecuteAsync(input);

            // Assert
            Assert.False(vehicle.IsRented);
            Assert.Null(vehicle.RentedByCustomerId);

            // Verifies that the repository's UpdateAsync method was called exactly once with the given vehicle
            repoMock.Verify(r => r.UpdateAsync(vehicle), Times.Once);

            // Verifies that the UnitOfWork's Save method was called exactly once to persist changes
            unitOfWorkMock.Verify(u => u.Save(), Times.Once);

            // Verifies that the output port handled a successful return with the expected VehicleId exactly once
            outputPortMock.Verify(o => o.StandardHandle(It.Is<ReturnVehicleOutput>(o => o.VehicleId == vehicleId)), Times.Once);

            // Verifies that the error output port (Invalid) was never called, meaning no error occurred
            outputInvalidMock.Verify(i => i.Invalid(It.IsAny<string>()), Times.Never);
        }
    }
}
