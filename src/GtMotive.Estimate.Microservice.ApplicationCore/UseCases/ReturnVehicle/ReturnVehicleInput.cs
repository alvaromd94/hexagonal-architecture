using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input data for return a vehicle.
    /// </summary>
    /// <param name="vehicleId">Identifier of the vehicle to return.</param>
    public class ReturnVehicleInput(
        Guid vehicleId) : IUseCaseInput
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;
    }
}
