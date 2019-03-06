using System;
using System.Threading.Tasks;
using HelloBotConsole.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole.Commands
{
    public class VideoCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public VideoCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task ExecuteCommand(MessageEventArgs e)
        {
            try
            {
                await _botClient.SendVideoAsync(
                    e.Message.Chat,
                    "https://github.com/TelegramBots/book/blob/master/src/docs/video-hawk.mp4");
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in VideoCommand: " + exception.Message);
            }
        }
    }
}