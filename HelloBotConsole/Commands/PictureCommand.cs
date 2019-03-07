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
    public class PictureCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public PictureCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        
        public async Task<Session> ExecuteCommand(MessageEventArgs e, Session session)
        {
            try
            {
                await _botClient.SendPhotoAsync(
                    chatId: e.Message.Chat,
                    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"
                );
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in PictureCommand: " + exception.Message);
            }
            
            return null;
        }
    }
}