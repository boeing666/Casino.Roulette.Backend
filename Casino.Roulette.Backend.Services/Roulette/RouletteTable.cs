using System.Collections.Concurrent;
using System.Collections.Generic;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Interfaces.Repository;
using Casino.Roulette.Backend.Services.Managers;

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
        public Queue<RouletteRound> RoundHistory { get; set; }

        private ConcurrentDictionary<long, UserOnTable> _connectedUsers;
        private ValidationManager ValidationManager;

        public RouletteTable(ValidationManager validationmanager, IMessageBroker messageBroker, IRoundRepository roundRepo)
        {
            _messageBroker = messageBroker;
            _roundRepo = roundRepo;
            _timer = new RouletteTimer(Constants.AfterRoundTime);
            _timer.Elapsed = BettingTimeStart;
            _connectedUsers = new ConcurrentDictionary<long, UserOnTable>();
            RoundHistory = new Queue<RouletteRound>(100);

            CurrentRound = _roundRepo.CreateNewRound();
            RoundHistory.Enqueue(CurrentRound);
            ValidationManager = validationmanager;

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

        public TableCurrentData GeTableCurrentData(long userId = 0)
        {
            return new TableCurrentData()
            {
                RoundId = this.RoundId,
                SecondsRemaining = _timer.TimeLeft,
                CurrentState = CurrentRound.State,
                PlayerBets = CurrentRound.GetPlayerBets(userId)
            };
        }

        public bool TakeBet(BetRequestModel betModel)
        {
            if (ValidationManager.ValidateBet(betModel, CurrentRound))
            {
                CurrentRound.PlayerBets.TryAdd(betModel.PlayerId, betModel);
            }


           
            
            return true;

        }
    }
}
