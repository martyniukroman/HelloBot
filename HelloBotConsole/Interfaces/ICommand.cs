using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace HelloBotConsole.Interfaces
{
    public interface ICommand
    {     
       Task ExecuteCommand(MessageEventArgs e);
    }
}