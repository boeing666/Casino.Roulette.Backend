using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class TableManager
    {
        private IMessageBroker _messageBroker;
        private UserManager _usernManager;
        private RouletteTimer _timer;

        public TableManager(IMessageBroker broker, UserManager userManager)
        {
            _messageBroker = broker;
            _usernManager = userManager;
            _timer = new RouletteTimer(Constants.AfterRoundTime);
            _timer.Elapsed = BettingTimeStart;
        }


        public void BettingTimeStart()
        {
            _messageBroker.BroadcastMessageToLobby(new object(), Commands.BettingTimeStart);

            _timer = new RouletteTimer(Constants.BettingTime);
            
        }


    }
}
