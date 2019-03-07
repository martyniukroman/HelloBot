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
    public class VideoCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public VideoCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task ExecuteCommand(MessageEventArgs e, Session session)
        {
            try
            {
                using (var stream = System.IO.File.OpenRead("../media/waves.mp4")) {
                    await _botClient.SendVideoNoteAsync(
                        chatId: e.Message.Chat,
                        videoNote: stream,
                        duration: 47,
                        length: 360 // value of width/height
                    );
                }
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in VideoCommand: " + exception.Message);
            }
        }
    }
}