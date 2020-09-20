using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;

namespace Casino.Roulette.Backend.Contracts.Models.Roulette
{
    public class RouletteRound
    {
        public long RoundId { get; set; }
        public RoundState State { get; set; }

        public ConcurrentDictionary<long, BetRequestModel> PlayerBets;

        public RouletteRound()
        {
            PlayerBets = new ConcurrentDictionary<long, BetRequestModel>();
            State = RoundState.None;
            //TODO here should go DB call for round ID or it will come from table class
        }


    }
}
