using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Interfaces.Repository;
using Casino.Roulette.Backend.Interfaces.Services;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class UserManager
    {
        private readonly ConcurrentDictionary<string, User> _connectedUsers;
        private readonly IUserService _userService;
        private readonly IMessageBroker _messageBroker;

        public UserManager(IUserService userService, IMessageBroker messageBroker)
        {
            _connectedUsers = new ConcurrentDictionary<string, User>();
            _userService = userService;
            _messageBroker = messageBroker;
        }


        public bool AddUser(string token, string connectionId, out User user)
        {
            user = new User();
            _messageBroker.SendMessageToUser(connectionId, new
            {
                user
            }, "JoinRoulette");
            return true;
        }

        public User GetUserByToken(string token)
        {
            try
            {
                return _userService.GetUserInfoByToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }


    }
}
