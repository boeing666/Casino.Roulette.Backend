using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces.Repository;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class UserManager
    {
        private readonly ConcurrentDictionary<long, User> _users;
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepo)
        {
            _users = new ConcurrentDictionary<long, User>();
            _userRepository = userRepo;
            FillData();
        }

        public User GetRandomUser()
        {
            return _userRepository.GetRandomUser();
        }

        public bool AddUser(string token, string connectionId, out User user)
        {
            user = null;

            if (_userRepository.TryGetUserByToken(token, out var newUser) == false)
            {
                return false;
            }

            if (_users.TryGetValue(newUser.Id, out var oldUser))
            {
                user = oldUser;
                oldUser.ConnectionId = connectionId;
                oldUser.Balance = newUser.Balance;
                return true;
            }
            else
            {
                _users.TryAdd(newUser.Id, newUser);
                return true;
            }

        }

        public User GetUserByToken(Guid token)
        {
            //player service should go here
            throw new NotImplementedException(); 
        }

        public bool GetUserById(long id, out User user)
        {
            return _users.TryGetValue(id, out user);
        }


        private void FillData()
        {
            foreach (var user in _userRepository.Initialize())
            {
                _users.TryAdd(user.Id, user);
            }
        }

    }
}
