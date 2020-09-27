using System.Collections.Concurrent;
using System.Collections.Generic;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Interfaces.Repository;

namespace Casino.Roulette.Backend.Services.Roulette
{
    public class RouletteTable
    {
        public long TableId { get; set; }

        private readonly IMessageBroker _messageBroker;
        private readonly IRoundRepository _roundRepo;

        private RouletteTimer _timer;

        public long RoundId { get; set; }
        public TableState TableState { get; set; }

        public RouletteRound CurrentRound { get; set; }
        public List<RouletteRound> RoundHistory { get; set; }

        private ConcurrentDictionary<long, UserOnTable> _connectedUsers;

        public RouletteTable(IMessageBroker messageBroker, IRoundRepository roundRepo)
        {
            _messageBroker = messageBroker;
            _roundRepo = roundRepo;
            _timer = new RouletteTimer(Constants.AfterRoundTime);
            _timer.Elapsed = BettingTimeStart;
            _connectedUsers = new ConcurrentDictionary<long, UserOnTable>();

            CurrentRound = _roundRepo.CreateNewRound();
        }
        public void BettingTimeStart()
        {
            _messageBroker.BroadcastMessageToLobby(GeTableCurrentData(), Commands.BettingTimeStart);

            _timer.SetInterval(Constants.BettingTime);
            _timer.Start();

        }

        public bool TryAddPlayerToTable(User user)
        {
            var userOnTable = new UserOnTable
            {
                TableId = TableId,
                Status = StatusOnTable.Spectating
            };

            return _connectedUsers.TryAdd(user.Id, userOnTable);
        }

        public TableCurrentData GeTableCurrentData()
        {
            return new TableCurrentData()
            {
                RoundId = this.RoundId,
                SecondsRemaining = _timer.TimeLeft,
                CurrentState = CurrentRound.State
            };
        }

        public bool TakeBet(BetRequestModel betModel)
        {
            if (CurrentRound.State != RoundState.RollingState)
            {
                return false;
            }

            return true;

        }
    }
}
