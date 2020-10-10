using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Interfaces.Repository;
using Microsoft.Extensions.Configuration;

namespace Casino.Roulette.Backend.Repository.Database
{
    public class RoundRepository : IRoundRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string DbConnection;
        public RoundRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            DbConnection = _configuration.GetConnectionString("DbConnection");
        }

        public RouletteRound CreateNewRound(long tableId)
        {
            long id = -1;
            using (var connection = new SqlConnection(DbConnection))
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
    }
}
