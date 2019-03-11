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
    public class HelpCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public HelpCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task<Session> ExecuteCommand(MessageEventArgs e, Session session)
        {
            if (e.Message.Text == "/basiccommands")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat,
                    "/text - `Getting text with info about author`\n" +
                    "/sticker - `Returns a sticker`\n" +
                    "/video - `Returns a video message`\n" +
                    "/picture - `Returns a picture`\n" +
                    "/audio - `Returns a song and an audio message`\n" +
                    "/stop - `Cancels current command and/or returns you to the main`\n" +
                    "", ParseMode.Markdown);
                return session;
            }

            if (e.Message.Text == "/workflowcommands")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat,
                    "/rootreboot - `Reboots a server machine`\n" +
                    "/help - `Gives you an information about commands`\n" +
                    "/form - `Apply form with your data`\n", ParseMode.Markdown);
                return session;
            }
            if (e.Message.Text == "/stop")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat, "/stop executed");
                session.Status = SessionStatus.Finished;
                return null;
            }

            await _botClient.SendTextMessageAsync(e.Message.Chat,
                "/basiccommands - `Basic Commands returns one message and no shit stuff`\n" +
                "/workflowcommands - `Workflow Commands have inner logic and can ask for additional values to interact with them`\n" +
                "", ParseMode.Markdown);

            return session;
        }
    }
}