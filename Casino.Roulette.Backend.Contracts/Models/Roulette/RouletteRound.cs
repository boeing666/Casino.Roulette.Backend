using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public ConcurrentDictionary<long, BetRequestModel> PlayerBets;

        private Random _random = new Random();

        private RouletteTimer _timer;

        public RouletteRound()
        {
            PlayerBets = new ConcurrentDictionary<long, BetRequestModel>();
            State = RoundState.None;
            _timer = new RouletteTimer(Constants.BallBouncingTime);
            _timer.Elapsed += GetResult;
        }

        public void GetResult()
        {
            var index = _random.Next(0, 37);
            Result = Constants.RouletteNumbers[index];
            CalculatePlayerWinsForRound();
        }
        public List<BetModel> GetPlayerBets(in long userId)
        {
            return new List<BetModel>();
        }

        private void CalculatePlayerWinsForRound()
        {
            foreach (var playerBets in PlayerBets.Values)
            {
                
            }
            
            
            
            
            
            
            
            
            
        }
    }
}
