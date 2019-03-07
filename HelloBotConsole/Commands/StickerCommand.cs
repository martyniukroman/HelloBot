using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace HelloBotConsole.Commands
{
    public class StickerCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public StickerCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task ExecuteCommand(MessageEventArgs e, Session session)
        {
            try
            {
                await _botClient.SendStickerAsync(
                    chatId: e.Message.Chat,
                    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                    );
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in StickerCommand: " + exception.Message);
            }

        }
    }
}