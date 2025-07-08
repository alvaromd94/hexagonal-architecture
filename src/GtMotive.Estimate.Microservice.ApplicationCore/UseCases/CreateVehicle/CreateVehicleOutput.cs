using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output data after creating a vehicle.
    /// </summary>
    public class CreateVehicleOutput(Guid vehicleId) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the vehicle ID.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;
    }
}
