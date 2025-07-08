using GtMotive.Estimate.Microservice.Api.Presenters;
using GtMotive.Estimate.Microservice.Api.UseCases.Common;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    /// <summary>
    /// Extension methods to configure presenters and use cases.
    /// </summary>
    public static class UserInterfaceExtensions
    {
        /// <summary>
        /// Adds presenters and use cases to the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            // Presenters
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<ListAvailableVehiclesPresenter>();
            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<ReturnVehiclePresenter>();

            services.AddScoped<IOutputPortStandard<CreateVehicleOutput>>(sp => sp.GetRequiredService<CreateVehiclePresenter>());
            services.AddScoped<IOutputPortInvalid>(sp => sp.GetRequiredService<CreateVehiclePresenter>());
            services.AddScoped<IOutputPortStandard<ListAvailableVehiclesOutput>>(sp => sp.GetRequiredService<ListAvailableVehiclesPresenter>());
            services.AddScoped<IOutputPortStandard<RentVehicleOutput>>(sp => sp.GetRequiredService<RentVehiclePresenter>());
            services.AddScoped<IOutputPortInvalid>(sp => sp.GetRequiredService<RentVehiclePresenter>());
            services.AddScoped<IOutputPortStandard<ReturnVehicleOutput>>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());
            services.AddScoped<IOutputPortInvalid>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());

            // Use Cases
            services.AddScoped<IUseCase<CreateVehicleInput>, CreateVehicleUseCase>();
            services.AddScoped<IUseCase<ListAvailableVehiclesInput>>(sp =>
                new ListAvailableVehiclesUseCase(
                    sp.GetRequiredService<IVehicleRepository>(),
                    sp.GetRequiredService<IOutputPortStandard<ListAvailableVehiclesOutput>>()));

            services.AddScoped<IUseCase<RentVehicleInput>>(sp =>
                new RentVehicleUseCase(
                    sp.GetRequiredService<IVehicleRepository>(),
                    sp.GetRequiredService<IUnitOfWork>(),
                    sp.GetRequiredService<IOutputPortStandard<RentVehicleOutput>>(),
                    sp.GetRequiredService<IOutputPortInvalid>()));

            services.AddScoped<IUseCase<ReturnVehicleInput>>(sp =>
                new ReturnVehicleUseCase(
                    sp.GetRequiredService<IVehicleRepository>(),
                    sp.GetRequiredService<IUnitOfWork>(),
                    sp.GetRequiredService<IOutputPortStandard<ReturnVehicleOutput>>(),
                    sp.GetRequiredService<IOutputPortInvalid>()));

            // Aggregated service classes
            services.AddScoped(sp => new VehicleUseCases(
                sp.GetRequiredService<IUseCase<CreateVehicleInput>>(),
                sp.GetRequiredService<IUseCase<ListAvailableVehiclesInput>>(),
                sp.GetRequiredService<IUseCase<RentVehicleInput>>(),
                sp.GetRequiredService<IUseCase<ReturnVehicleInput>>()));

            services.AddScoped(sp => new VehiclePresenters(
                sp.GetRequiredService<CreateVehiclePresenter>(),
                sp.GetRequiredService<ListAvailableVehiclesPresenter>(),
                sp.GetRequiredService<RentVehiclePresenter>(),
                sp.GetRequiredService<ReturnVehiclePresenter>()));

            return services;
        }
    }
}
