using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Presenter for RentVehicle use case.
    /// </summary>
    public class RentVehiclePresenter : IOutputPortStandard<RentVehicleOutput>, IOutputPortInvalid
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RentVehicleOutput response)
        {
            ActionResult = new OkObjectResult(response);
        }

        public void Invalid(string message)
        {
            ActionResult = new BadRequestObjectResult(new { error = message });
        }
    }
}
