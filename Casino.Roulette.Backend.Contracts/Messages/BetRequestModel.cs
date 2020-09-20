using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Messages
{
    public class BetRequestModel
    {
        public List<BetModel> Bets { get; set; }
        public long PlayerId { get; set; }
        public string ConnectionId { get; set; }
        public Guid Token { get; set; }
        public long TableId { get; set; }

        public decimal TotalBetAmount
        {
            get
            {
               return Bets.Sum(x => x.BetAmount);
            } 
        }
    }

    public class BetModel
    {
        public int BetIndex { get; set; }
        public decimal BetAmount { get; set; }
    }
}
