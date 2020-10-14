using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Models.Roulette
{
    public class RoundWinResultModel
    {
        public long PlayerId { get; set; }
        public long RoundId { get; set; }
        public decimal TotalBetAmount { get; set; }
        public decimal TotalWinAmount { get; set; }
    }
}
