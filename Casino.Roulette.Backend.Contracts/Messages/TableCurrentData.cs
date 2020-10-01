using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;

namespace Casino.Roulette.Backend.Contracts.Messages
{
    public class TableCurrentData
    {
        public TableCurrentData()
        {
            PlayerBets = new List<BetModel>();
        }
        public RoundState CurrentState { get; set; }
        public long RoundId { get; set; }
        public long SecondsRemaining { get; set; }
        
        public List<BetModel> PlayerBets { get; set; }
    }
}
