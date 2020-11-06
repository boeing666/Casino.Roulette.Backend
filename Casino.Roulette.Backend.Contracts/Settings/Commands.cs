using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Settings
{
    public class Commands
    {
        public static string StateUpdate { get; set; } = "StateUpdated";
        public static string BettingTimeStart { get; set; } = "BettingTimeStart";

    }
}
