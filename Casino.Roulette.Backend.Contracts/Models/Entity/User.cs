using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Models.Entity
{
    public class User
    {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public string Username { get; set; }


        public string ConnectionId { get; set; }


        public UserModel GetUserModel()
        {
            return new UserModel()
            {
                Name = Username,
                Balance = Balance,
            };
        }
    }
}
