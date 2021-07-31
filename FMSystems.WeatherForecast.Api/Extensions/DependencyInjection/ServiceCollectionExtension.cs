﻿using FMSystems.WeatherForecast.Domain.Repository;
using FMSystems.WeatherForecast.Infrastructure.Api.RepositoryImpl;
using FMSystems.WeatherForecast.Infrastructure.Db.Context;
using FMSystems.WeatherForecast.Infrastructure.Db.RepositoryImpl;
using FMSystems.WeatherForecast.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// A services collection extension to encapulate the dependency injection operations.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Sets up the dependency injection configuration.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IServiceCollection"/> for creating and configuring the system.</returns>
        public static IServiceCollection AddWeatherForecastApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //Database
            services.AddDbContext<WeatherForecastDbContext>(options => options.UseInMemoryDatabase(":memory:"));

            //DB context
            services.AddScoped<IWeatherForecastDbContext, WeatherForecastDbContext>();

            //Services.
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IForecastRepository, ForecastRepository>();

            return services;
        }
    }
}