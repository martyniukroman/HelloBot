using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using HelloBotConsole.Commands;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace HelloBotConsole
{
    class Program
    {
        private static ITelegramBotClient botClient;
        private static StickerCommand _stickerCommand;
        private static TextCommand _textCommand;
        private static VideoCommand _videoCommand;
        private static PictureCommand _pictureCommand;
        private static RebootCommand _rebootCommand;
        private static AudioCommand _audioCommand;
        private static HelpCommand _helpCommand;
        private static FormCommand _formCommand;

        private static List<Session> Sessions = new List<Session>();
        private static Session _requestedSession = null;

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
            _helpCommand = new HelpCommand(botClient);
            _formCommand = new FormCommand(botClient);


            botClient.OnMessage += BotOnMessage;
            botClient.OnMessageEdited += BotOnMessage;
            botClient.OnCallbackQuery += BotOnQuery;
            botClient.StartReceiving();


            Console.WriteLine($"Hello World I'm the:  ID:{me.Id} / FirstName:{me.FirstName} / Username:{me.Username}!");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Bye..");
            System.Threading.Thread.Sleep(1000);
        }


        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Received a text message in chat: {e.Message.Chat.Id}" +
                              $"\nWith user: {e.Message.Chat.Username}" +
                              $"\nWith message: {e.Message.Text}");

            try
            {
                if (_requestedSession == null)
                {
                    try
                    {
                        switch (e.Message.Text)
                        {
                            case "/text":
                                await _textCommand.ExecuteCommand(e, null);
                                break;
                            case "/sticker":
                                await _stickerCommand.ExecuteCommand(e, null);
                                break;
                            case "/video":
                                await botClient.SendChatActionAsync(e.Message.Chat, ChatAction.RecordVideo);
                                await _videoCommand.ExecuteCommand(e, null);
                                break;
                            case "/picture":
                                await _pictureCommand.ExecuteCommand(e, null);
                                break;
                            case "/rootreboot":
                                Sessions.Add(new Session(_rebootCommand, e.Message.Chat.Id, "/rootreboot",
                                    SessionStatus.Undefined));
                                _requestedSession = await _rebootCommand.ExecuteCommand(e, Sessions.Last());
                                break;
                            case "/audio":
                                await botClient.SendChatActionAsync(e.Message.Chat, ChatAction.UploadAudio);
                                await _audioCommand.ExecuteCommand(e, null);
                                break;
                            case "/help":
                                Sessions.Add(
                                    new Session(_helpCommand,
                                        e.Message.Chat.Id, "/help",
                                        SessionStatus.Undefined));

                                _requestedSession = await _helpCommand.ExecuteCommand(e, Sessions.Find( (s) => s.SessionChatId == e.Message.Chat.Id));
                                break;
                            case "/form":
                                Sessions.Add(new Session(_formCommand, e.Message.Chat.Id, "/form", SessionStatus.Undefined));
                                _requestedSession = await _formCommand.ExecuteCommand(e, Sessions.Find( (s) => s.SessionChatId == e.Message.Chat.Id));
                                break;
                            default:
                                await botClient.SendTextMessageAsync(
                                    e.Message.Chat, $"Use /help to get additional info",
                                    parseMode: ParseMode.Markdown);
                                break;
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
                else
                {
                    var searchSession = Sessions.Find((s) => s.SessionChatId == _requestedSession.SessionChatId);

                    if (_requestedSession != null)
                    {
                        _requestedSession =
                            await _requestedSession.CommandSessionHandler.ExecuteCommand(e, _requestedSession);

                        if (_requestedSession == null)
                        {
                            Sessions.Remove(searchSession);
                        }
                    }
                    else
                    {
                        Sessions.Remove(searchSession);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static async void BotOnQuery(object sender, CallbackQueryEventArgs callback)
        {

            var query = callback.CallbackQuery;
            
            await botClient.AnswerCallbackQueryAsync(query.Id,  $"Received {query.Data}");
            
            await botClient.SendTextMessageAsync(
                query.Message.Chat.Id,
                $"Callback Received {query.Data}");
        }
        
    }
}