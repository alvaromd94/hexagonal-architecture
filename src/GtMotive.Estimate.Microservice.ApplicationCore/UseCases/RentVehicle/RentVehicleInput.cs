using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input data for renting a vehicle.
    /// </summary>
    /// <param name="vehicleId">Identifier of the vehicle to rent.</param>
    /// <param name="customerId">Identifier of the customer renting the vehicle.</param>
    public class RentVehicleInput(
        Guid vehicleId,
        string customerId) : IUseCaseInput
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public string CustomerId { get; } = customerId;
    }
}
