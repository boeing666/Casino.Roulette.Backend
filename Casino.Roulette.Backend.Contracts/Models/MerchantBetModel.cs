using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Models
{
    public class MerchantBetModel
    {
        public decimal BetAmount { get; set; }
        public long PlayerId { get; set; }
    }
}
