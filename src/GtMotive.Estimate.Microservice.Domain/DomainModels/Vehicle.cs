using System;

namespace GtMotive.Estimate.Microservice.Domain.DomainModels
{
    /// <summary>
    /// Represents a vehicle in the renting fleet.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the unique identifier of the vehicle.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the brand of the vehicle.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacture date of the vehicle.
        /// </summary>
        public DateTime? ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vehicle is currently rented.
        /// </summary>
        public bool IsRented { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer who rented the vehicle, if any.
        /// </summary>
        public string RentedByCustomerId { get; set; }
    }
}
