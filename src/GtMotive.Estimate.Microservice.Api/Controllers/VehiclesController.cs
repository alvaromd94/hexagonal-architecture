using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.Common;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// API controller for managing vehicles.
    /// </summary>
    [ApiController]
    [Route("vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleUseCases _useCases;
        private readonly VehiclePresenters _presenters;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="useCases">Encapsulated use cases related to vehicle operations.</param>
        /// <param name="presenters">Encapsulated presenters for formatting use case responses.</param>
        public VehiclesController(VehicleUseCases useCases, VehiclePresenters presenters)
        {
            ArgumentNullException.ThrowIfNull(useCases);
            ArgumentNullException.ThrowIfNull(presenters);

            _useCases = useCases;
            _presenters = presenters;
        }

        /// <summary>
        /// Gets a list of available vehicles in the fleet.
        /// </summary>
        /// <returns>List of available vehicles.</returns>
        [HttpGet("availables")]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            await _useCases.ListAvailable.ExecuteAsync(new ListAvailableVehiclesInput());

            return Ok(_presenters.ListAvailable.Vehicles);
        }

        /// <summary>
        /// Creates a new vehicle in the fleet.
        /// </summary>
        /// <param name="request">Request to create the vehicle.</param>
        /// <returns>Action result containing the operation outcome.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _useCases.Create.ExecuteAsync(request.ToInput());
            return _presenters.Create.ActionResult;
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _useCases.Rent.ExecuteAsync(request.ToInput());
            return _presenters.Rent.ActionResult;
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _useCases.Return.ExecuteAsync(request.ToInput());
            return _presenters.Return.ActionResult;
        }
    }
}
