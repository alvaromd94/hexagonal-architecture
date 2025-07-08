using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Output data after get a available vehicle list.
    /// </summary>
    public class ListAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets the vehicle ID.
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
    }
}
