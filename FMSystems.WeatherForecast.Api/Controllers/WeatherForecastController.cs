﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using FMSystems.WeatherForecast.Domain.Repository;

namespace FMSystems.WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IForecastRepository _forecastRepository;
        private readonly ICityRepository _cityRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastRepository forecastRepository, ICityRepository cityRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _forecastRepository = forecastRepository ?? throw new ArgumentNullException(nameof(forecastRepository));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IEnumerable<FMSystems.WeatherForecast.Domain.Entity.WeatherForecast>> Get()
        {
            return _forecastRepository.GetForecasts();
        }
    }
}
