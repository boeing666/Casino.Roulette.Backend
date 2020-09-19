using Casino.Roulette.Backend.Services.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Contracts.Messages;

namespace Casino.Roulette.Backend.Core
{
    public class RouletteEngine : IRouletteEngine
    {
        private readonly UserManager _userManager;
        private readonly TableManager _tableManager;
        public IMessageBroker MessageBroker { get; }
        public RouletteEngine(UserManager usernaManager, TableManager tableManager, IMessageBroker broker)
        {
            _userManager = usernaManager;
            MessageBroker = broker;
            _tableManager = tableManager;
        }

        public User GetRandomUserFromUserRepo()
        {
            return _userManager.GetRandomUser();
        }

        public bool ConnectToRoulette(string token, string connectionId)
        {
            return _userManager.AddUser(token, connectionId, out var user);
        }

        public User GetUserInfoByToken(Guid token)
        {
            return _userManager.GetUserByToken(token);
        }

        public bool TryConnectToRouletteTable(long tableId, User user)
        {

            return _tableManager.TryGetTableById(tableId, out var table) && table.TryAddPlayerToTable(user);
        }

        public bool TryGetUserByConnectionId(string connection, out User user)
        {
            return _userManager.TryGetUser(connection, out user);
        }

        public TableCurrentData GetTableData(long tableId)
        {
            return !_tableManager.TryGetTableById(tableId, out var table) ? null : table.GeTableCurrentData();
        }
    }
}
