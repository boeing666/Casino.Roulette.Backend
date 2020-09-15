using Casino.Roulette.Backend.Services.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces;

namespace Casino.Roulette.Backend.Core
{
    public class RouletteEngine : IRouletteEngine
    {
        private readonly UserManager _userManager;
        private readonly TableManager _tableManager;
        public IMessageBroker MessageBroker { get; }
        public RouletteEngine(UserManager usernaManager, TableManager tableManager, IMessageBroker broker)
        {
            _userManager = usernaManager;
            MessageBroker = broker;
            _tableManager = tableManager;
        }

        public User GetRandomUserFromUserRepo()
        {
            return _userManager.GetRandomUser();
        }

        public bool ConnectToRoulette(string token, string connectionId)
        {
            return _userManager.AddUser(token, connectionId, out var user);
        }
    }
}
