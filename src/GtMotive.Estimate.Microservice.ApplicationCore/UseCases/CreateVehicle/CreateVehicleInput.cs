using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input data for creating a new vehicle.
    /// </summary>
    /// <param name="licensePlate">License plate of the vehicle.</param>
    /// <param name="brand">Brand of the vehicle.</param>
    /// <param name="model">Model of the vehicle.</param>
    /// <param name="manufactureDate">Manufacture date of the vehicle.</param>
    public class CreateVehicleInput(
        string licensePlate,
        string brand,
        string model,
        DateTime manufactureDate) : IUseCaseInput
    {
        /// <summary>
        /// Gets the license plate.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the brand.
        /// </summary>
        public string Brand { get; } = brand;

        /// <summary>
        /// Gets the model.
        /// </summary>
        public string Model { get; } = model;

        /// <summary>
        /// Gets the manufacture date.
        /// </summary>
        public DateTime ManufactureDate { get; } = manufactureDate;
    }
}
