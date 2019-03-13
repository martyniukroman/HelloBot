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

    public abstract class CommandAbstract : ICommand
    {
        public string CommandKey;
        public abstract Task<Session> ExecuteCommand(MessageEventArgs e, Session session);
    }
}