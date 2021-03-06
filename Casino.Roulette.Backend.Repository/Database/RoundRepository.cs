﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Interfaces.Repository;
using Microsoft.Extensions.Configuration;

namespace Casino.Roulette.Backend.Repository.Database
{
    public class RoundRepository : IRoundRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;
        public RoundRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("RouletteConnection");
        }

        public RouletteRound CreateNewRound(long tableId)
        {
            long id = -1;
            using (var connection = new SqlConnection(_connection))
            {
                var command = new SqlCommand()
                {
                    CommandText = "sp_create_new_round",
                    CommandType = CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@table_id", tableId);
                command.Parameters["@round_id"].Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                id = Convert.ToInt64(command.Parameters["@round_id"].Value);
            }
            
            return new RouletteRound()
            {
                RoundId = id
            };
        }

        public async void SaveRoundResults(List<RoundWinResultModel> currentRoundRoundResult)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(10000);
                //todo pokershi rogorc gavakete table igive logika
                Console.WriteLine("Finished saving bets");
            });

        }
    }
}
