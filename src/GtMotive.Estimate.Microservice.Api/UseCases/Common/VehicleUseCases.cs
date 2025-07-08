using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Common
{
    /// <summary>
    /// Aggregates all use cases related to vehicle operations.
    /// </summary>
    /// <param name="create">Use case for creating a vehicle.</param>
    /// <param name="listAvailable">Use case for listing available vehicles.</param>
    /// <param name="rent">Use case for renting a vehicle.</param>
    /// <param name="ret">Use case for returning a vehicle.</param>
    public class VehicleUseCases(
        IUseCase<CreateVehicleInput> create,
        IUseCase<ListAvailableVehiclesInput> listAvailable,
        IUseCase<RentVehicleInput> rent,
        IUseCase<ReturnVehicleInput> ret)
    {
        /// <summary>Gets the use case for creating a vehicle.</summary>
        public IUseCase<CreateVehicleInput> Create { get; } = create;

        /// <summary>Gets the use case for listing available vehicles.</summary>
        public IUseCase<ListAvailableVehiclesInput> ListAvailable { get; } = listAvailable;

        /// <summary>Gets the use case for renting a vehicle.</summary>
        public IUseCase<RentVehicleInput> Rent { get; } = rent;

        /// <summary>Gets the use case for returning a vehicle.</summary>
        public IUseCase<ReturnVehicleInput> Return { get; } = ret;
    }
}
