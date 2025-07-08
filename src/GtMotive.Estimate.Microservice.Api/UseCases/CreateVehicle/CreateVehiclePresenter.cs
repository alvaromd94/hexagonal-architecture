using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Presenter for the <see cref="CreateVehicleUseCase"/>.
    /// Transforms the output of the use case into HTTP responses.
    /// </summary>
    public class CreateVehiclePresenter :
        IOutputPortStandard<CreateVehicleOutput>,
        IOutputPortInvalid,
        IWebApiPresenter
    {
        /// <inheritdoc />
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc />
        public void StandardHandle(CreateVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new CreatedResult(
                $"/vehicles/{response.VehicleId}",
                response);
        }

        /// <inheritdoc />
        public void Invalid(string message)
        {
            ActionResult = new BadRequestObjectResult(new { Error = message });
        }
    }
}
