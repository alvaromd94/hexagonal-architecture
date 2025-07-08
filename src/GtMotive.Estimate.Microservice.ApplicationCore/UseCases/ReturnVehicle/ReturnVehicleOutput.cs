using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output after return a vehicle.
    /// </summary>
    public class ReturnVehicleOutput(Guid vehicleId) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the vehicle ID.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;
    }
}
