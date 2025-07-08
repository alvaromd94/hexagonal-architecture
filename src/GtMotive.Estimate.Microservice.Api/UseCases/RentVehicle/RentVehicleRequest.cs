using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Request to rent a vehicle.
    /// </summary>
    public class RentVehicleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the vehicle to rent.
        /// </summary>
        [Required]
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer renting the vehicle.
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Converts the request to a use case input.
        /// </summary>
        /// <returns>The corresponding <see cref="RentVehicleInput"/>.</returns>
        public RentVehicleInput ToInput()
        {
            return !VehicleId.HasValue
                ? throw new InvalidOperationException("VehicleId is required.")
                : new RentVehicleInput(VehicleId.Value, CustomerId);
        }
    }
}
