using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using HelloBotConsole;

                // System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
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

        public async Task<Session> ExecuteCommand(MessageEventArgs e, Session session)
        {
            _currentSesion = session;
            try
            {
                _botClient.OnMessage += SubmitReboot;
                await _botClient.SendTextMessageAsync(e.Message.Chat,
                    "This command is for root only,\n`Prove your identity`", parseMode: ParseMode.Markdown);
                
                    _currentSesion.Status = SessionStatus.Started;
              
            }
            catch (Exception exception)
            {
                throw new Exception("Exception occured in RebootCommand: " + exception.Message);
            }
            return _currentSesion;
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
            else
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "Use /stop to exit");
            }
            
            if (_currentSesion.Status == SessionStatus.Finished)
            {
                _botClient.OnMessage -= SubmitReboot;
            }
             
            
        }
    }
}