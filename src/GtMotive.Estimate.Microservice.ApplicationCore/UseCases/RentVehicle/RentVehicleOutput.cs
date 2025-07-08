using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output after renting a vehicle.
    /// </summary>
    public class RentVehicleOutput(Guid vehicleId) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the vehicle ID.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;
    }
}
