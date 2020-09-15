using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Casino.Roulette.Backend.Repository.Mocking
{
    public class MockUserRepository : IUserRepository
    {
        private readonly Random _random;
        private long _userCount = 10000;

        //private readonly ILogger _logger;
        public MockUserRepository(/*ILogger logger*/)
        {
            _random = new Random();
            //_logger = logger;
        }

        public User GetRandomUser()
        {
            var user = new User()
            {
                Id = _random.Next(1000, 10000),
                Balance = _random.Next(100),
                Username = $"User{_random.Next(30)}"
            };
            return user;
        }

        public List<User> Initialize()
        {
            var list = new List<User>();
            list.Add(new User()
            {
                Id = _userCount,
                Balance = 12331,
                Username = "zulu"
            });
            _userCount++;
            for (int i = 0; i < 15; i++)
            {
                list.Add(new User()
                {
                    Id = _userCount,
                    Balance = 12331,
                    Username = "user"+ _userCount
                });
                _userCount++;
            }

            return list;
        }

        public bool TryGetUserByToken(string token, out User user)
        {
            user = null;
            if (token == null)
            {
                return false;
            }
            user = new User()
            {
                Id = _random.Next(1000, 10000),
                Balance = _random.Next(100),
                Username = token
            };

            return true;

        }



    }
}
