﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AWSApiGateway.Annotations;

namespace SampleApp9000.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [XAmazonApiGatewayIntegration(PassthroughBehavior = PassthroughBehavior.WHEN_NO_MATCH)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController()
        {
        }

        [HttpGet]
        [XAmazonApiGatewayIntegration(ConnectionType = ConnectionType.INTERNET)]
        [XAmazonApiGatewayRequestValidator(RequestValidator = "params-only")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        [XAmazonApiGatewayIntegration(ConnectionType = ConnectionType.INTERNET)]
        //
        // Path parameters are automatically mapped but may be overridden by explicitly mapping them by
        // using the XAmazonApiGatewayIntegrationRequestParameter as demonstrated below.
        //
        //[XAmazonApiGatewayIntegrationRequestParameter("integration.request.querystring.stage","method.request.querystring.version")]
        //[XAmazonApiGatewayIntegrationRequestParameter("integration.request.path.op","method.request.path.service")]
        public IEnumerable<WeatherForecast> GetById(int id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
