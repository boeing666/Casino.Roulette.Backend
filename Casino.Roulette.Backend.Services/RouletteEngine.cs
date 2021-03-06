﻿using System;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Services.Managers;

namespace Casino.Roulette.Backend.Services
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

        public bool ConnectToRoulette(string token, string connectionId)
        {
            return _userManager.AddUser(token, connectionId, out var user);
        }

        public bool TryGetUserByConnectionId(string connection, out User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserInfoByToken(string token)
        {
            return _userManager.GetUserByToken(token);
        }

        public bool TryConnectToRouletteTable(long tableId, User user)
        {
            return _tableManager.TryGetTableById(tableId, out var table) && table.TryAddPlayerToTable(user);
        }

        public TableCurrentData GetTableData(long tableId, long userId)
        {
            return !_tableManager.TryGetTableById(tableId, out var table) ? null : table.GeTableCurrentData(userId);
        }
        

        public bool TakePlayerBet(BetRequestModel betModel)
        {
            if (!_tableManager.TryGetTableById(betModel.TableId, out var table))
            {
                return false;
            }

            return table.TakeBet(betModel);
        }

    }
}
