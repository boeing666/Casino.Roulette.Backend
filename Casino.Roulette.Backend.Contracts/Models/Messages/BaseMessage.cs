using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Roulette.Backend.Contracts.Models.Messages
{
    public class BaseMessage
    {
        public string Command { get; set; }
        public object Data { get; set; }

        public static BaseMessage GetMessage(object messageObject, string command)
        {
            return new BaseMessage()
            {
                Command = command,
                Data = messageObject,
            };
        }
    }
}
