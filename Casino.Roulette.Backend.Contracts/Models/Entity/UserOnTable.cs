using System;
using System.Collections.Generic;
using System.Text;
using Casino.Roulette.Backend.Contracts.Enums;

namespace Casino.Roulette.Backend.Contracts.Models.Entity
{
    public class UserOnTable : User
    {
        public long TableId { get; set; }

        public StatusOnTable Status{ get; set; }
    }
}
