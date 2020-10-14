using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts;
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
        private RouletteTimer _stateTimer;
        public long RoundId { get; set; }
        public TableState TableState { get; set; }
        public RouletteRound CurrentRound { get; set; }
        public Queue<RouletteRound> RoundHistory { get; set; }

        private ConcurrentDictionary<long, UserOnTable> _connectedUsers;
        private readonly ValidationManager ValidationManager;

        public RouletteTable(ValidationManager validationmanager, IMessageBroker messageBroker, IRoundRepository roundRepo)
        {
            _messageBroker = messageBroker;
            _roundRepo = roundRepo;
            _stateTimer = new RouletteTimer(Constants.AfterRoundTime) {Elapsed = BettingTimeStart};
            _stateTimer.Start();
            _connectedUsers = new ConcurrentDictionary<long, UserOnTable>();
            RoundHistory = new Queue<RouletteRound>(100);

            CurrentRound = _roundRepo.CreateNewRound(TableId);
            RoundHistory.Enqueue(CurrentRound);
            ValidationManager = validationmanager;

        }
        public void BettingTimeStart()
        {
            _messageBroker.BroadcastMessageToLobby(GeTableCurrentData(), Commands.BettingTimeStart);
            ChangeTimerSettings(Constants.BettingTime, BettingTimeFinished);
        }

        public void BettingTimeFinished()
        {
            CurrentRound.State = RoundState.RollingState;
            ChangeTimerSettings(Constants.BallBouncingTime, GetResult);
            
        }

        public void GetResult()
        {
           CurrentRound.GetResult();
           SaveRoundResult(CurrentRound);
           CreateNewRound();
        }

        private void CreateNewRound()
        {
            CurrentRound = _roundRepo.CreateNewRound(TableId);
            ChangeTimerSettings(Constants.AfterRoundTime, BettingTimeStart);
        }

        private  void SaveRoundResult(RouletteRound currentRound)
        {
            //await Task.Run(() =>
            //{
            //    _roundRepo.SaveRoundResults(currentRound.RoundResult);
            //});

            _roundRepo.SaveRoundResults(currentRound.RoundResult);
            Console.WriteLine("Creating new round");

            //this doesnt work 
        }

        private void ChangeTimerSettings(long time, Delegates.VoidMethod elapseHandler)
        {
            _stateTimer.SetInterval(time);
            _stateTimer.Elapsed = elapseHandler;
            _stateTimer.Start();
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
                SecondsRemaining = _stateTimer.TimeLeft,
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
