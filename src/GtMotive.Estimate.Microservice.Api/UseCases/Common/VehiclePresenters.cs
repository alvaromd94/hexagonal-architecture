using GtMotive.Estimate.Microservice.Api.Presenters;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Common
{
    /// <summary>
    /// Aggregates all presenters for vehicle-related operations.
    /// </summary>
    /// <param name="create">The presenter for creating vehicles.</param>
    /// <param name="listAvailable">The presenter for listing available vehicles.</param>
    /// <param name="rent">The presenter for renting vehicles.</param>
    /// <param name="ret">The presenter for returning vehicles.</param>
    public class VehiclePresenters(
        CreateVehiclePresenter create,
        ListAvailableVehiclesPresenter listAvailable,
        RentVehiclePresenter rent,
        ReturnVehiclePresenter ret)
    {
        /// <summary>Gets the presenter for creating vehicles.</summary>
        public CreateVehiclePresenter Create { get; } = create;

        /// <summary>Gets the presenter for listing available vehicles.</summary>
        public ListAvailableVehiclesPresenter ListAvailable { get; } = listAvailable;

        /// <summary>Gets the presenter for renting vehicles.</summary>
        public RentVehiclePresenter Rent { get; } = rent;

        /// <summary>Gets the presenter for returning vehicles.</summary>
        public ReturnVehiclePresenter Return { get; } = ret;
    }
}
