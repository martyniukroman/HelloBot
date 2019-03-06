using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole.Commands
{
    public class RebootCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public RebootCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        
        public async Task ExecuteCommand(MessageEventArgs e)
        {
            try
            {
                _botClient.OnMessage += SubmitReboot;
               await _botClient.SendTextMessageAsync(e.Message.Chat, "This command is for root only");
              // await _botClient.SendTextMessageAsync(e.Message.Chat, "Are you sure to reboot server machine?");
                // System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
              //  SubmitReboot(null,e);
                _botClient.OnMessage -= SubmitReboot;
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in RebootCommand: " + exception.Message);
            }
        }

        private async void SubmitReboot(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "1" || e.Message.Text == "yes")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "REBOOTING");
            }
            else
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "Rebooting doesn\'t execute");
            }
            
        }
        
    }
}