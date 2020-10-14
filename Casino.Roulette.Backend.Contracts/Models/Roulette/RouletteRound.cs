using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Settings;

namespace Casino.Roulette.Backend.Contracts.Models.Roulette
{
    public class RouletteRound
    {
        public long RoundId { get; set; }
        public RoundState State { get; set; }
        public RouletteNum Result { get; set; }
        public List<RoundWinResultModel> RoundResult { get; set; }

        public ConcurrentDictionary<long, BetRequestModel> PlayerBets;

        private Random _random = new Random();

        private readonly RouletteTimer _stateTimer;
        private readonly RouletteTimer _bouncingTimer;

        public RouletteRound()
        {
            PlayerBets = new ConcurrentDictionary<long, BetRequestModel>();
            RoundResult = new List<RoundWinResultModel>();
            State = RoundState.BettingTime;
        }

        public void BettingTimeFinished()
        {
            State = RoundState.RollingState;
            _bouncingTimer.Start();
        }

        public void GetResult()
        {
            var index = _random.Next(0, 37);
            Result = Constants.RouletteNumbers[index];

            RoundResult = CalculatePlayerWinsForRound();
        }
        public List<BetModel> GetPlayerBets(in long userId)
        {
            return new List<BetModel>();
        }

        private List<RoundWinResultModel> CalculatePlayerWinsForRound()
        {
            var winResultsForRound = new List<RoundWinResultModel>();
            foreach (var playerBet in PlayerBets.Values)
            {
                var result = new RoundWinResultModel {RoundId = RoundId, PlayerId = playerBet.PlayerId};
                //todo needs to be finish
                var winFromNumber = playerBet.Bets.FirstOrDefault(x => x.BetIndex == Result.Number)?.BetAmount * 36;
                var winFromColor = playerBet.Bets.FirstOrDefault(x => x.BetIndex == (int) Result.Color)?.BetAmount * 2;
                var winFrom2X = playerBet.Bets.FirstOrDefault(x => (x.BetIndex == 42 && Result.Number % 2 == 0) || (x.BetIndex ==43 && Result.Number%2==1))
                    ?.BetAmount * 2;
                winResultsForRound.Add(result);
            }
            return winResultsForRound;
        }
    }
}
