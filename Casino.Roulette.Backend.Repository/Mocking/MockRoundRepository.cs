using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async void SaveRoundResults(List<RoundWinResultModel> currentRoundResult)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(10000);
                RoundResultRepository.AddRange(currentRoundResult);
                Console.WriteLine("Mock Finished saving bets");
            });

        }

    }
}
