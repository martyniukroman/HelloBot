using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
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

        public async Task ExecuteCommand(MessageEventArgs e)
        {
            try
            {
                await _botClient.SendStickerAsync(
                    e.Message.Chat,
                    "https://github.com/TelegramBots/book/blob/master/src/docs/sticker-dali.webp");
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in StickerCommand: " + exception.Message);
            }

        }
    }
}