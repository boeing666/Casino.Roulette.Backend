using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Settings
{
    public class Commands
    {
        public static string StateUpdate { get; set; } = "state_updated";
        public static string BettingTimeStart { get; set; } = "betting_start";

    }
}
