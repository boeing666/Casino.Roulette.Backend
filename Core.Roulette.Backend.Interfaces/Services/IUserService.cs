using Casino.Roulette.Backend.Contracts.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Interfaces.Services
{
    public interface IUserService
    {
        User GetUserInfoByToken(string token);
    }
}
