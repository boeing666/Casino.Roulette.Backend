using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Interfaces.Repository;

namespace Casino.Roulette.Backend.Repository.Mocking
{
    public class MockRoundRepository : IRoundRepository
    {
        public static long RoundId { get; set; }

        public RouletteRound CreateNewRound()
        {
            return new RouletteRound()
            {
                RoundId = ++RoundId,

            };
        }
    }
}
