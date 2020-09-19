using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Messages
{
    public class RouletteBettingTimeMessage
    {
        public long RoundId { get; set; }
        public long MaxTime { get; set; }
        public long RemainingDuration { get; set; }
    }
}
