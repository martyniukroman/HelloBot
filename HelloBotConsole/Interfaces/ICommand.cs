using System.Threading.Tasks;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace HelloBotConsole.Interfaces
{
    public interface ICommand
    {     
        Task<Session> ExecuteCommand(MessageEventArgs e, Session session);
    }
}