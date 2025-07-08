using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Use case for retrieving a list of available vehicles.
    /// </summary>
    public class ListAvailableVehiclesUseCase : IUseCase<ListAvailableVehiclesInput>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOutputPortStandard<ListAvailableVehiclesOutput> _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="outputPort">The output port.</param>
        public ListAvailableVehiclesUseCase(
            IVehicleRepository vehicleRepository,
            IOutputPortStandard<ListAvailableVehiclesOutput> outputPort)
        {
            ArgumentNullException.ThrowIfNull(vehicleRepository);
            ArgumentNullException.ThrowIfNull(outputPort);

            _vehicleRepository = vehicleRepository;
            _outputPort = outputPort;
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(ListAvailableVehiclesInput input)
        {
            var vehicles = await _vehicleRepository.GetAvailableVehiclesAsync();

            foreach (var vehicle in vehicles)
            {
                _outputPort.StandardHandle(new ListAvailableVehiclesOutput
                {
                    Id = vehicle.Id,
                    LicensePlate = vehicle.LicensePlate,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    ManufactureDate = vehicle.ManufactureDate,
                });
            }
        }
    }
}
