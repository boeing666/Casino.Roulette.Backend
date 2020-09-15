using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Models.Entity;

namespace Casino.Roulette.Backend.Core
{
    public interface IRouletteEngine
    {
        User GetRandomUserFromUserRepo();
        bool ConnectToRoulette(string token, string connectionId);

        User GetUserInfoByToken(Guid token);
    }
}
