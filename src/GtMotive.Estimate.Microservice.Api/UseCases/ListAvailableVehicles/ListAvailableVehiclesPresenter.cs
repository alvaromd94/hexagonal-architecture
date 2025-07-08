using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Presenter for ListAvailableVehicles use case.
    /// </summary>
    public class ListAvailableVehiclesPresenter : IOutputPortStandard<ListAvailableVehiclesOutput>
    {
#pragma warning disable IDE0028 // Simplify collection initialization
        private readonly List<ListAvailableVehiclesOutput> _vehicles = new();
#pragma warning restore IDE0028 // Simplify collection initialization

        /// <summary>
        /// Gets the list of available vehicles returned by the use case.
        /// </summary>
        public IReadOnlyCollection<ListAvailableVehiclesOutput> Vehicles => _vehicles;

        /// <inheritdoc />
        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            _vehicles.Add(response);
        }
    }
}
