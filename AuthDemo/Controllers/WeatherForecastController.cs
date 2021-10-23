using AuthDemo.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

    [Authorize(Roles =UserRoles.Admin)]
    //[Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Log.Information("Admin is accessing Get method in WeatherForecast");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("List")]
        [HttpGet]
        public IEnumerable<int> List()
        {
            var lst = new List<int>();
            for (var i = 0; i < 21; i++)
                lst.Add(i);
            Log.Warning("NotFoundException will be thrown because of default behavious");
            throw new NotFoundException();
            return lst;
        }
    }
}
