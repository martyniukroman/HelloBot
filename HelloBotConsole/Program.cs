using System;
using System.Net.Security;
using HelloBotConsole.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole
{
    class Program
    {
        public static ITelegramBotClient botClient;

        private static StickerCommand _stickerCommand;
        private static TextCommand _textCommand;
        private static VideoCommand _videoCommand;
        private static PictureCommand _pictureCommand;
        private static RebootCommand _rebootCommand;
        private static AudioCommand _audioCommand;

        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("760620360:AAGq9KIBMccVIGo0Rbggq3Hn88ehgLVNq38");
            var me = botClient.GetMeAsync().Result;

            _stickerCommand = new StickerCommand(botClient);
            _textCommand = new TextCommand(botClient);
            _videoCommand = new VideoCommand(botClient);
            _pictureCommand = new PictureCommand(botClient);
            _rebootCommand = new RebootCommand(botClient);
            _audioCommand = new AudioCommand(botClient);


            botClient.OnMessage += BotOnMessage;
            botClient.StartReceiving();


            Console.WriteLine($"Hello World I'm the:  ID:{me.Id} / FirstName:{me.FirstName} / Username:{me.Username}!");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Bye..");
            System.Threading.Thread.Sleep(1000);
        }


        public static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Received a text message in chat: {e.Message.Chat.Id}" +
                              $"\nWith user: {e.Message.Chat.Username}" +
                              $"\nWith message: {e.Message.Text}");

//            await botClient.SendTextMessageAsync(e.Message.Chat, $"Message: {e.Message.Text} \n" +
//                                                                 $"Sender: {e.Message.From} \n" +
//                                                                 $"Date: {e.Message.Date} \n"


            try
            {
                if (e.Message.Text == "/text")
                {
                    await _textCommand.ExecuteCommand(e);
                }
                else if (e.Message.Text == "/sticker")
                {
                    await _stickerCommand.ExecuteCommand(e);
                }
                else if (e.Message.Text == "/video")
                {
                    await botClient.SendChatActionAsync(e.Message.Chat, ChatAction.RecordVideo);
                    await _videoCommand.ExecuteCommand(e);
                }
                else if (e.Message.Text == "/picture")
                {
                    await _pictureCommand.ExecuteCommand(e);
                }
                else if (e.Message.Text == "/rootreboot")
                {
                    await botClient.SendChatActionAsync(e.Message.Chat, ChatAction.UploadDocument);
                    botClient.OnMessage -= BotOnMessage;
                    await _rebootCommand.ExecuteCommand(e);
                    botClient.OnMessage += BotOnMessage;
                }
                else if (e.Message.Text == "/audio")
                {
                    await botClient.SendChatActionAsync(e.Message.Chat, ChatAction.UploadAudio);
                    await _audioCommand.ExecuteCommand(e);
                }
                else
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, "*Commands*:\n " +
                                                                         "/text \n " +
                                                                         "/sticker \n " +
                                                                         "/video \n " +
                                                                         "/picture \n " +
                                                                         "/audio \n " +
                                                                         "" +
                                                                         "" +
                                                                         "/rootreboot \n " +
                                                                         "", ParseMode.Markdown);
                }
            }
            catch (Exception exception)
            {
                await botClient.SendTextMessageAsync(
                    e.Message.Chat, $"*{exception.Message}*",
                    parseMode: ParseMode.Markdown);

                Console.WriteLine($"*{exception.Message}*");
            }
        }
    }
}