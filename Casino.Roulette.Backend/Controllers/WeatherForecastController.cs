using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Core;
using Casino.Roulette.Backend.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Casino.Roulette.Backend.Controllers
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
        private readonly IUserRepository _userRepository;
        private readonly IRouletteEngine _engine;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserRepository repo, IRouletteEngine engine)
        {
            _logger = logger;
            _userRepository = repo;
            _engine = engine;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var list = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                var userFromRepo = _engine.GetRandomUserFromUserRepo();
                list.Add(userFromRepo);
            }

            return list;
        }

    }
}
