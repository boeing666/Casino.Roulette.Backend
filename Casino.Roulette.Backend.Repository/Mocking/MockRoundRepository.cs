using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Interfaces.Repository;

namespace Casino.Roulette.Backend.Repository.Mocking
{
    public class MockRoundRepository : IRoundRepository
    {
        public static long RoundId { get; set; } = 10000;
        public static List<RoundWinResultModel> RoundResultRepository { get; set; } = new List<RoundWinResultModel>();

        public RouletteRound CreateNewRound(long tableId)
        {
            return new RouletteRound()
            {
                RoundId = ++RoundId
            };
        }

        public void SaveRoundResults(List<RoundWinResultModel> currentRoundResult)
        {
            RoundResultRepository.AddRange(currentRoundResult);
        }

    }
}
