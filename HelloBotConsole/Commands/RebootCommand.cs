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

            switch (e.Message.Text)
            {
                case "1":
                    await _botClient.SendTextMessageAsync(e.Message.Chat, "rebooting");
                    _currentSesion.Status = SessionStatus.Finished;
                    return null;
                case "/stop":
                    await _botClient.SendTextMessageAsync(e.Message.Chat, "/stop executed");
                    _currentSesion.Status = SessionStatus.Finished;
                    return null;
                default:
                    await _botClient.SendTextMessageAsync(e.Message.Chat,
                        "You are in /rootreboot method\nUse /stop to exit\n`prove your identity`", ParseMode.Markdown);
                    break;
            }

            return _currentSesion;
        }
    }
}