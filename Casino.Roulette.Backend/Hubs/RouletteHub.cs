using System.Threading.Tasks;
using Casino.Roulette.Backend.Core;
using Microsoft.AspNetCore.SignalR;

namespace Casino.Roulette.Backend.Hubs
{
    public class RouletteHub : Hub
    {
        private readonly IRouletteEngine _engine;

        public RouletteHub(IRouletteEngine engine)
        {
            _engine = engine;
        }


        public override async Task OnConnectedAsync()
        {
            var requestQuery = Context.GetHttpContext().Request.Query;
            if (requestQuery.ContainsKey("token"))
            {
                var token = requestQuery["token"];
                _engine.ConnectToRoulette(token, Context.ConnectionId);
            }
        }



    }
}
