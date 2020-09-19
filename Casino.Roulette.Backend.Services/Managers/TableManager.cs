using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class TableManager
    {
        private IMessageBroker _messageBroker;
        private UserManager _usernManager;
        private RouletteTimer _timer;

        private ConcurrentDictionary<long, RouletteTable> _rouletteTables;

        public TableManager(IMessageBroker broker, UserManager userManager)
        {
            _messageBroker = broker;
            _usernManager = userManager;
            _rouletteTables = new ConcurrentDictionary<long, RouletteTable>();

        }

        public bool TryGetTableById(long tableId, out RouletteTable table)
        {
            return _rouletteTables.TryGetValue(tableId, out table);
        }


    }
}
