using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Roulette;

namespace Casino.Roulette.Backend.Interfaces.Repository
{
    public interface IRoundRepository
    {
        RouletteRound CreateNewRound(long tableId);
        void SaveRoundResults(List<RoundWinResultModel> currentRoundRoundResult);
    }
}
