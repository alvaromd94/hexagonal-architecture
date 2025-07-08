using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Request DTO for creating a new vehicle.
    /// </summary>
    public class CreateVehicleRequest
    {
        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the brand of the vehicle.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacture date of the vehicle.
        /// </summary>
        [Required]
        public DateTime? ManufactureDate { get; set; }

        /// <summary>
        /// Maps this request to the use case input.
        /// </summary>
        /// <returns>The use case input.</returns>
        public CreateVehicleInput ToInput()
        {
            return new CreateVehicleInput(
                LicensePlate,
                Brand,
                Model,
                ManufactureDate.Value);
        }
    }
}
