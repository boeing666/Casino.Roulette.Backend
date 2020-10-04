using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models;
using Casino.Roulette.Backend.Contracts.Models.Messages.Request;
using Casino.Roulette.Backend.Services;
using Casino.Roulette.Backend.Services.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Roulette.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRouletteEngine _engine;
        private readonly MerchantManager _manager;

        public TestController(IRouletteEngine engine, MerchantManager manager)
        {
            _engine = engine;
            _manager = manager;
        }
        //[HttpGet("/zxcv")]
        //public IActionResult Lol(GetUserInfoModel model)
        //{
        //   return Ok(_engine.GetUserInfoByToken(model.Token));
        //}


        [HttpGet("/asdasd")]
        public IActionResult Asdasd()
        {
            _manager.PostBet(new MerchantBetModel(){BetAmount = 10, PlayerId = 213});
            return Ok();
        }
    }
}
