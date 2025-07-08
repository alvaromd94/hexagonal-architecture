using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle to a customer.
    /// </summary>
    public class RentVehicleUseCase : IUseCase<RentVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutputPortStandard<RentVehicleOutput> _outputPort;
        private readonly IOutputPortInvalid _outputInvalid;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for data access.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions.</param>
        /// <param name="outputPort">The output port for success responses.</param>
        /// <param name="outputInvalid">The output port for validation failures.</param>
        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            IOutputPortStandard<RentVehicleOutput> outputPort,
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
        /// Executes the use case logic for renting a vehicle.
        /// </summary>
        /// <param name="input">The input data containing customer and vehicle identifiers.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var alreadyRented = await _vehicleRepository.ExistsRentedByCustomerAsync(input.CustomerId);
            if (alreadyRented)
            {
                _outputInvalid.Invalid("The customer already has a rented vehicle.");
                return;
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
            if (vehicle is null)
            {
                _outputInvalid.Invalid("Vehicle not found.");
                return;
            }

            if (vehicle.IsRented)
            {
                _outputInvalid.Invalid("Vehicle is already rented.");
                return;
            }

            vehicle.IsRented = true;
            vehicle.RentedByCustomerId = input.CustomerId;

            await _vehicleRepository.UpdateAsync(vehicle);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(new RentVehicleOutput(vehicle.Id));
        }
    }
}
