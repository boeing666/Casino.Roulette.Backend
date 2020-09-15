using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Messages;
using Casino.Roulette.Backend.Hubs;
using Casino.Roulette.Backend.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Casino.Roulette.Backend.Models
{
    public class HubMessageBroker : IMessageBroker
    {
        private readonly IHubContext<RouletteHub> _rouletteHub;

        public HubMessageBroker(IHubContext<RouletteHub> rouletteHub)
        {
            _rouletteHub = rouletteHub;
        }


        public Task SendMessageToUser(string connectionId, object messageObject, string command)
        {
            return _rouletteHub.Clients.Client(connectionId).SendAsync(command, messageObject);
        }

        public Task BroadcastMessageToLobby(object messageObject, string command)
        {
            return _rouletteHub.Clients.All.SendAsync(command, messageObject);
        }


        private BaseMessage GetBaseMessage(object messageObject, string command)
        {
            return new BaseMessage()
            {
                Command = command,
                Data = messageObject,
            };
        }
    }
}
