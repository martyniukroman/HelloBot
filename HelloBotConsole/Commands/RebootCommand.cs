using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole.Commands
{
    public class RebootCommand : ICommand
    {
        private ITelegramBotClient _botClient;
        private Session _currentSesion;

        public RebootCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task ExecuteCommand(MessageEventArgs e, Session session)
        {
            _currentSesion = session;
            try
            {
                _botClient.OnMessage += SubmitReboot;
                await _botClient.SendTextMessageAsync(e.Message.Chat,
                    "This command is for root only,\n`Prove your identity`", parseMode: ParseMode.Markdown);
                // await _botClient.SendTextMessageAsync(e.Message.Chat, "Are you sure to reboot server machine?");
                // System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");

                do
                {
                    _currentSesion.Status = SessionStatus.InProgress;
                   SubmitReboot(null, e);
                   System.Threading.Thread.Sleep(5000);
                } while (_currentSesion.Status != SessionStatus.Finished);

                _botClient.OnMessage -= SubmitReboot;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception occured in RebootCommand: " + exception.Message);
            }
        }

        private async void SubmitReboot(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "1" || e.Message.Text == "yes")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "REBOOTING");
                _currentSesion.Status = SessionStatus.Finished;
            }
            else if (e.Message.Text == "/stop")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "Rebooting doesn\'t execute");
                _currentSesion.Status = SessionStatus.Finished;
            }
           
        }
    }
}