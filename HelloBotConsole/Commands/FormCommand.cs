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
            if (e.Message.Text == "/stop" || session.Status == SessionStatus.Finished)
            {
                this.user = new UserModel();
                return null;
            }

            if (session.Status == SessionStatus.Undefined)
            {
                session.Status = SessionStatus.Started;

                if (user.Name == null)
                {
                    await _botClient.SendTextMessageAsync(e.Message.Chat, "Enter your name");
                    return session;
                }

                if (user.Age == null && user.Name != null)
                {
                    await _botClient.SendTextMessageAsync(e.Message.Chat, "Enter age");
                    return session;
                }
            }
            else if (session.Status == SessionStatus.Started)
            {
                session.Status = SessionStatus.Undefined;

                if (user.Name == null)
                {
                    user.Name = e.Message.Text;
                    return session;
                }

                if (user.Age == null && user.Name != null)
                {
                    
                    try
                    {
                        user.Age = Convert.ToInt32(e.Message.Text);
                    }
                    catch (Exception exception)
                    {
                        await _botClient.SendTextMessageAsync(e.Message.Chat, exception.Message);
                        return session;
                    }

                    {
                        await _botClient.SendTextMessageAsync(e.Message.Chat, $"Name: {user.Name}\n" +
                                                                              $"Age: {user.Age}");
                        session.Status = SessionStatus.Finished;
                        this.user = new UserModel();
                        return null;
                    }
                }
            }
            
            return session;
        }
    }
}