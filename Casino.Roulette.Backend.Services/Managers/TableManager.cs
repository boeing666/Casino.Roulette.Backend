using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Roulette;
using Casino.Roulette.Backend.Contracts.Settings;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Services.Roulette;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class TableManager
    {
        private IMessageBroker _messageBroker;
        private UserManager _usernManager;
        private IServiceProvider _serviceProvider;
        private ConcurrentDictionary<long, RouletteTable> _rouletteTables;

        public TableManager(IMessageBroker broker, IServiceProvider serviceProvider, UserManager userManager)
        {
            _messageBroker = broker;
            _usernManager = userManager;
            _serviceProvider = serviceProvider;
            _rouletteTables = new ConcurrentDictionary<long, RouletteTable>();
            var table = (RouletteTable)_serviceProvider.GetService(typeof(RouletteTable));

            _rouletteTables.TryAdd(table.TableId, table);
        }

        public bool TryGetTableById(long tableId, out RouletteTable table)
        {
            return _rouletteTables.TryGetValue(tableId, out table);
        }


    }
}
