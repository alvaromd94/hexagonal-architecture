using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Domain.DomainModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    [Collection(TestCollections.Functional)]
    public sealed class RentVehicleUseCaseTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task ExecuteAsyncShouldRentAvailableVehicle()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var customerId = Guid.NewGuid().ToString();

            var vehicle = new Vehicle
            {
                Id = vehicleId,
                Brand = "Volkswagen",
                Model = "Polo",
                LicensePlate = "1234ABC",
                ManufactureDate = DateTime.UtcNow.AddYears(-3),
                IsRented = false,
                RentedByCustomerId = null
            };

            await Fixture.UsingRepository<IVehicleRepository>(async repo =>
            {
                await repo.AddAsync(vehicle);
            });

            var input = new RentVehicleInput(vehicleId, customerId);

            // Act
            await Fixture.UsingRepository<IUseCase<RentVehicleInput>>(async useCase =>
            {
                await useCase.ExecuteAsync(input);
            });

            // Assert
            await Fixture.UsingRepository<IVehicleRepository>(async repo =>
            {
                var updatedVehicle = await repo.GetByIdAsync(vehicleId);

                Assert.NotNull(updatedVehicle);
                Assert.True(updatedVehicle!.IsRented);
                Assert.Equal(customerId, updatedVehicle.RentedByCustomerId);
            });
        }
    }
}
