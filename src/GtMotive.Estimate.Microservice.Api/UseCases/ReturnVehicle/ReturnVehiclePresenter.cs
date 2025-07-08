using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Presenters
{
    /// <summary>
    /// Presenter for ReturnVehicle use case.
    /// </summary>
    public class ReturnVehiclePresenter : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortInvalid
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ReturnVehicleOutput response)
        {
            ActionResult = new OkObjectResult(response);
        }

        public void Invalid(string message)
        {
            ActionResult = new BadRequestObjectResult(new { error = message });
        }
    }
}
