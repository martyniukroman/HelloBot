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
    public class AudioCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public AudioCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        
        public async Task<Session> ExecuteCommand(MessageEventArgs e, Session session)
        {
            try
            {
                await _botClient.SendAudioAsync(e.Message.Chat, 
                    "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
                );
                
                using (var stream = System.IO.File.OpenRead("../media/voice.ogg")) {
                    await _botClient.SendVoiceAsync(
                        chatId: e.Message.Chat,
                        voice: stream,
                        duration: 36
                    );
                }
                
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in AudioCommand: " + exception.Message);
            }

            return null;
        }

    }
}