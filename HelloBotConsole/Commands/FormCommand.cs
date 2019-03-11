using System;
using System.Linq;
using System.Threading.Tasks;
using HelloBotConsole.Helpers;
using HelloBotConsole.Interfaces;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole.Commands
{
    public class FormCommand : ICommand
    {
        private ITelegramBotClient _botClient;
        private UserModel user = new UserModel();

        public FormCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task<Session> ExecuteCommand(MessageEventArgs e, Session session)
        {
//            if (e.Message.Text == "/stop")
//            {
//                await _botClient.SendTextMessageAsync(e.Message.Chat, "/stop executed");
//                session.Status = SessionStatus.Finished;
//                return null;
//            }
//
//            if (e.Message.Text.Contains("/name"))
//            {
//                user.Name = e.Message.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();
//            }
//
//            if (e.Message.Text.Contains("/age"))
//            {
//                user.Age = Convert.ToInt32(e.Message.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last());
//            }
//
//            if (e.Message.Text.Contains("/submit"))
//            {
//                await _botClient.SendTextMessageAsync(e.Message.Chat,
//                    "Your credentials: \n" +
//                    $"   `/name` {user.Name}\n" +
//                    $"   `/age`  {user.Age}\n", ParseMode.Markdown);
//
//                session.Status = SessionStatus.Finished;
//                
//                return null;
//            }
//
//            var inline = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Enter your name", "name"));
//            
//            await _botClient.SendTextMessageAsync(e.Message.Chat,
//                "Enter your name and age in next format in separate messages: \n" +
//                $"   /name `yourname:` {user.Name}\n" +
//                $"   /age `yourage:`  {user.Age}\n" +
//                "use /submit for applying the `/form`\n" +
//                "use /stop to abort `/form`\n", ParseMode.Markdown, replyMarkup: inline);

            if (session.Status == SessionStatus.Undefined)
            {
                session.Status = SessionStatus.Started;
                return session;
            }

            if (session.Status == SessionStatus.Started)
            {
                user.Name = await InputHelper.Insert("Enter your name below: ", e);
                user.Age = Convert.ToInt32(await InputHelper.Insert("Enter age name below: ", e));
            }

            return session;
        }
    }
}