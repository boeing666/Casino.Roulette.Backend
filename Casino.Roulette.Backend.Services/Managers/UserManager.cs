using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces.Repository;
using Casino.Roulette.Backend.Interfaces.Services;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class UserManager
    {
        private readonly ConcurrentDictionary<string, User> _connectedUsers;
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _connectedUsers = new ConcurrentDictionary<string, User>();
            _userService = userService;
        }


        public bool AddUser(string token, string connectionId, out User user)
        {
            user = new User();
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
