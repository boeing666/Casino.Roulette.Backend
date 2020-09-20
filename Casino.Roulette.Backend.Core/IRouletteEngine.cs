using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;

namespace Casino.Roulette.Backend.Core
{
    public interface IRouletteEngine
    {
        User GetRandomUserFromUserRepo();
        bool ConnectToRoulette(string token, string connectionId);
        bool TryGetUserByConnectionId(string connection, out User user);


        User GetUserInfoByToken(Guid token);
        bool TryConnectToRouletteTable(long tableId, User user);
        TableCurrentData GetTableData(long tableId);
        bool TakePlayerBet(BetRequestModel betModel);
    }
}
