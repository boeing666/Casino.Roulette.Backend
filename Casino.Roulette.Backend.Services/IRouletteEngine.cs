using System;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;

namespace Casino.Roulette.Backend.Services
{
    public interface IRouletteEngine
    {
        bool ConnectToRoulette(string token, string connectionId);
        bool TryGetUserByConnectionId(string connection, out User user);


        User GetUserInfoByToken(string token);
        bool TryConnectToRouletteTable(long tableId, User user);
        TableCurrentData GetTableData(long tableId);
        bool TakePlayerBet(BetRequestModel betModel);
    }
}
