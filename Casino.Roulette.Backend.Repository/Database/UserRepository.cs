using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces.Repository;

namespace Casino.Roulette.Backend.Repository.Database
{
    public class UserRepository : IUserRepository
    {
        public User GetRandomUser()
        {
            throw new NotImplementedException();
        }

        public User GetUserByToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool TryGetUserByToken(string token, out User user)
        {
            throw new NotImplementedException();
        }
    }
}
