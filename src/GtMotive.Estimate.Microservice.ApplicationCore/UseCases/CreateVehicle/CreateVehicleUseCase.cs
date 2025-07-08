using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.DomainModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Use case for creating a new vehicle.
    /// </summary>
    public class CreateVehicleUseCase : IUseCase<CreateVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutputPortStandard<CreateVehicleOutput> _outputPort;
        private readonly IOutputPortInvalid _outputInvalid;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="outputPort">The output port for returning results.</param>
        /// <param name="outputInvalid">The output port invalid.</param>
        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            IOutputPortStandard<CreateVehicleOutput> outputPort,
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

        /// <inheritdoc />
        public async Task ExecuteAsync(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var exists = await _vehicleRepository.ExistsByLicensePlateAsync(input.LicensePlate);
            if (exists)
            {
                _outputInvalid.Invalid("A vehicle with the same license plate already exists.");
                return;
            }

            var age = DateTime.UtcNow.Year - input.ManufactureDate.Year;
            if (age > 5 || input.ManufactureDate > DateTime.UtcNow)
            {
                _outputInvalid.Invalid("The manufacture date is too old or in the future.");
                return;
            }

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                LicensePlate = input.LicensePlate,
                Brand = input.Brand,
                Model = input.Model,
                ManufactureDate = input.ManufactureDate,
                IsRented = false
            };

            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(new CreateVehicleOutput(vehicle.Id));
        }
    }
}
