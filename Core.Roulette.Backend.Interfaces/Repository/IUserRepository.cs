using Casino.Roulette.Backend.Contracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Interfaces.Repository
{
    public interface IUserRepository
    {
        bool TryGetUserByToken(string token, out User user);

        User GetRandomUser();

        List<User> Initialize();
    }
}
