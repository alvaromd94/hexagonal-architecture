using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.DomainModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    internal sealed class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly ConcurrentDictionary<Guid, Vehicle> _vehicles = new();

        public Task AddAsync(Vehicle vehicle)
        {
            _vehicles[vehicle.Id] = vehicle;
            return Task.CompletedTask;
        }

        public Task<Vehicle> GetByIdAsync(Guid id)
        {
            _vehicles.TryGetValue(id, out var vehicle);
            return Task.FromResult(vehicle);
        }

        public Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            var available = _vehicles.Values.Where(v => !v.IsRented);
            return Task.FromResult<IEnumerable<Vehicle>>(available.ToList());
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            if (_vehicles.ContainsKey(vehicle.Id))
            {
                _vehicles[vehicle.Id] = vehicle;
            }

            return Task.CompletedTask;
        }

        public Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            var exists = _vehicles.Values.Any(v => v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        public Task<bool> ExistsRentedByCustomerAsync(string customerId)
        {
            var exists = _vehicles.Values.Any(v => v.IsRented && v.RentedByCustomerId == customerId);
            return Task.FromResult(exists);
        }
    }
}
