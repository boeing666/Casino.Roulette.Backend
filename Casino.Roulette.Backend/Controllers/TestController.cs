using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Messages.Request;
using Casino.Roulette.Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Roulette.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRouletteEngine _engine;

        public TestController(IRouletteEngine engine)
        {
            _engine = engine;
        }
        [HttpGet]
        public IActionResult Test(GetUserInfoModel model)
        {
           return Ok(_engine.GetUserInfoByToken(model.Token));
        }
    }
}
