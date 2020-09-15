using System.Threading.Tasks;

namespace Casino.Roulette.Backend.Interfaces
{
    public interface IMessageBroker
    {
        Task SendMessageToUser(string connectionId, object messageObject, string command);
        Task BroadcastMessageToLobby(object messageObject, string command);
    }
}
