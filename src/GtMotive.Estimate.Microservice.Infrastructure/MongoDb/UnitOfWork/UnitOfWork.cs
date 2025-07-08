using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.UnitOfWork
{
    /// <summary>
    /// Unit of Work implementation for coordinating MongoDB operations.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository to manage vehicle persistence.</param>
    public class UnitOfWork(IVehicleRepository vehicleRepository) : IUnitOfWork
    {
        /// <summary>
        /// Gets the vehicle repository.
        /// </summary>
        public IVehicleRepository VehicleRepository { get; } = vehicleRepository;

        /// <summary>
        /// Applies all pending changes.
        /// For MongoDB, operations are committed immediately, so this is a no-op.
        /// </summary>
        /// <returns>
        /// Always returns 0, as there is no transactional persistence in this implementation.
        /// </returns>
        public Task<int> Save()
        {
            return Task.FromResult(0);
        }
    }
}
