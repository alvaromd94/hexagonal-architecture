using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.DomainModels;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines the contract for accessing and managing vehicles in the fleet.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the fleet.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Retrieves a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The matching vehicle if found; otherwise, null.</returns>
        Task<Vehicle> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all vehicles in the fleet that are not currently rented.
        /// </summary>
        /// <returns>A collection of available vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Updates an existing vehicle, such as its rental status.
        /// </summary>
        /// <param name="vehicle">The vehicle with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Checks whether a vehicle with the specified license plate exists.
        /// </summary>
        /// <param name="licensePlate">The license plate to check.</param>
        /// <returns>True if the vehicle exists; otherwise, false.</returns>
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);

        /// <summary>
        /// Checks whether the specified customer currently has a rented vehicle.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>True if the customer has an active rental; otherwise, false.</returns>
        Task<bool> ExistsRentedByCustomerAsync(string customerId);
    }
}
