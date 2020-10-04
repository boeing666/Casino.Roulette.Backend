using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Roulette;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class ValidationManager
    {
        public bool ValidateBet(BetRequestModel betModel, RouletteRound currentRound)
        {
            return betModel.TotalBetAmount > 0 && !betModel.Bets.Any(x => x.BetAmount < 0) &&
                currentRound.State == RoundState.None;
        }
    }
}
