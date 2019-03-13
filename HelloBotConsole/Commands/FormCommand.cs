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
                await _botClient.SendTextMessageAsync(e.Message.Chat, "/stop executed");
                return null;
            }

            if (user.Name == null)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "Enter Name");
                if (!e.Message.Text.Contains("/"))
                {
                    foreach (var item in e.Message.Text)
                    {
                        if (!char.IsLetter(item))
                        {
                            return session;
                        }
                    }
                    user.Name = e.Message.Text;
                }
            }   
            

            if (user.Age == null && user.Name != null)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "Enter Age");
                if (!e.Message.Text.Contains("/"))
                {
                    foreach (var item in e.Message.Text)
                    {
                        if (!char.IsDigit(item))
                        {
                            return session;
                        }
                    }

                    user.Age = Convert.ToInt32(e.Message.Text);
                }
            }

            if (user.Age != null && user.Name != null)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, $"Name: {user.Name} \n" +
                                                                      $"Age: {user.Age} \n");
            }

            return session;
        }
    }
}