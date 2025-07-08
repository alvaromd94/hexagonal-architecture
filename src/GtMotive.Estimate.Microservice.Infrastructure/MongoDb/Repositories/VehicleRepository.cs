using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.DomainModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB implementation of the <see cref="IVehicleRepository"/> interface.
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The MongoDB service.</param>
        /// <param name="options">The MongoDB settings.</param>
        public VehicleRepository(IOptions<MongoDbSettings> options, MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            var settings = options?.Value ?? throw new ArgumentNullException(nameof(options));

            var database = mongoService.MongoClient.GetDatabase(settings.MongoDbDatabaseName);
            _collection = database.GetCollection<Vehicle>("vehicles");
        }

        /// <inheritdoc />
        public async Task AddAsync(Vehicle vehicle)
        {
            await _collection.InsertOneAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.IsRented, false);
            return await _collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicle.Id);
            await _collection.ReplaceOneAsync(filter, vehicle);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.LicensePlate, licensePlate);
            return await _collection.Find(filter).AnyAsync();
        }

        /// <inheritdoc />
        public async Task<bool> ExistsRentedByCustomerAsync(string customerId)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.RentedByCustomerId, customerId),
                Builders<Vehicle>.Filter.Eq(v => v.IsRented, true));

            return await _collection.Find(filter).AnyAsync();
        }
    }
}
