using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Request to return a vehicle.
    /// </summary>
    public class ReturnVehicleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the vehicle to return.
        /// </summary>
        [Required]
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// Converts the request to a use case input.
        /// </summary>
        /// <returns>The corresponding <see cref="ReturnVehicleInput"/>.</returns>
        public ReturnVehicleInput ToInput()
        {
            return !VehicleId.HasValue
                ? throw new InvalidOperationException("VehicleId is required.")
                : new ReturnVehicleInput(VehicleId.Value);
        }
    }
}
