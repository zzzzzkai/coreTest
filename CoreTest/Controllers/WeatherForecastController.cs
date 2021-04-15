using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Service.IService;

namespace CoreTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class WeatherForecastController : ControllerBase
    {
        
        private readonly IUserService _UserService;



        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };



        public WeatherForecastController(IUserService userService)
        {


            _UserService = userService;
        }

        [HttpGet]
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

        [HttpGet("UserList")]
        
        public List<User> UserList()
        {
            return (List<User>) _UserService.getAll();
        }
    }
}
