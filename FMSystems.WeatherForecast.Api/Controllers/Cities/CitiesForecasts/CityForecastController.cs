﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FMSystems.WeatherForecast.Domain.Repository;
using FMSystems.WeatherForecast.Domain.Entity;

namespace FMSystems.WeatherForecast.Api.Controllers.Cities.CitiesForecast
{
    /// <summary>
    /// A controller that is responsible for cities's forecasts endpoints.
    /// </summary>
    [ApiController]
    [Route("cities/{cityId:int}/forecast")]
    [ResponseCache(Duration = 60)]
    public class CityForecastController: ControllerBase
    {
        private readonly ILogger<CityForecastController> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IForecastRepository _forecastRepository;

        /// <summary>
        /// The CityForecastController constructor.
        /// </summary>
        /// <param name="logger">the logger object.</param>
        /// <param name="cityRepository">the city repository.</param>
        /// <param name="forecastRepository">the forecast repository.</param>
        public CityForecastController(ILogger<CityForecastController> logger, ICityRepository cityRepository, IForecastRepository forecastRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _forecastRepository = forecastRepository ?? throw new ArgumentNullException(nameof(forecastRepository));
        }

        /// <summary>
        /// Returns the forecast for a given city id or 404 case it doesn't exist.
        /// </summary>
        /// <param name="cityId">the city id.</param>
        /// <param name="date">the date or 07/04/2018 at noon if nothing is passed.</param>
        /// <returns>A list of cities or empty if none exists.<see cref="City"/></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = nameof(GetAsync))]
        public async Task<ActionResult<Forecast>> GetAsync(int cityId, DateTime? date)
        {
            _logger.LogDebug($"{nameof(GetAsync)} starting...");

            var city = await _cityRepository.GetByIdAsync(cityId);
            if (city == null) return NotFound("The city was not found for the given city id.");

            var forecast = await _forecastRepository.GetForecastAsync(city.Latitude, city.Longitude, date);
            if (!forecast.IsValid) return BadRequest("No valid weather information was found for the given date and time.");

            return Ok(forecast);
        }
    }
}
