using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;

namespace Casino.Roulette.Backend.Services
{
    public class RouletteTable
    {
        public long TableId { get; set; }
        private readonly IMessageBroker _messageBroker;
        private RouletteTimer _timer;

        private ConcurrentDictionary<long, UserOnTable> _connectedUsers;
        public RouletteTable(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker; 

            _timer = new RouletteTimer(Constants.AfterRoundTime);
            _timer.Elapsed = BettingTimeStart;
            _connectedUsers = new ConcurrentDictionary<long, UserOnTable>();
        }
        public void BettingTimeStart()
        {
            _messageBroker.BroadcastMessageToLobby(new object(), Commands.BettingTimeStart);

            _timer.SetInterval(Constants.BettingTime);
            _timer.Start();

        }

        public bool TryAddPlayerToTable(User user)
        {
            var userOnTable = new UserOnTable();
            userOnTable.TableId = TableId;
            userOnTable.Status = StatusOnTable.Spectating;

            return _connectedUsers.TryAdd(user.Id, userOnTable);
        }


    }
}
