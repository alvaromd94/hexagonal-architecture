using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Use case for renting a vehicle to a customer.
    /// </summary>
    public class ReturnVehicleUseCase : IUseCase<ReturnVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutputPortStandard<ReturnVehicleOutput> _outputPort;
        private readonly IOutputPortInvalid _outputInvalid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for data access.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions.</param>
        /// <param name="outputPort">The output port for success responses.</param>
        /// <param name="outputInvalid">The output port for validation failures.</param>
        public ReturnVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            IOutputPortStandard<ReturnVehicleOutput> outputPort,
            IOutputPortInvalid outputInvalid)
        {
            ArgumentNullException.ThrowIfNull(vehicleRepository);
            ArgumentNullException.ThrowIfNull(unitOfWork);
            ArgumentNullException.ThrowIfNull(outputPort);

            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
            _outputInvalid = outputInvalid;
        }

        /// <summary>
        /// Executes the use case logic for return a vehicle.
        /// </summary>
        /// <param name="input">The input data containing customer and vehicle identifiers.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
            if (vehicle is null)
            {
                _outputInvalid.Invalid("Vehicle not found.");
                return;
            }

            if (!vehicle.IsRented)
            {
                _outputInvalid.Invalid("Vehicle is not currently rented.");
                return;
            }

            vehicle.IsRented = false;
            vehicle.RentedByCustomerId = null;

            await _vehicleRepository.UpdateAsync(vehicle);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(new ReturnVehicleOutput(vehicle.Id));
        }
    }
}
