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
    public class TextCommand : ICommand
    {
        private ITelegramBotClient _botClient;

        public TextCommand(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        
        public async Task ExecuteCommand(MessageEventArgs e, Session session)
        {
            try
            {
                await _botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"*Message*: `{e.Message.Text}`\n" + $"*Sender*: `{e.Message.From}`\n" + $"*Date*: `{e.Message.Date}`\n",
                    parseMode: ParseMode.Markdown,
                    replyToMessageId: e.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                        "Author",
                        "https://t.me/martcs"
                    ))
                );
            }
            catch (Exception exception)
            {
                throw  new Exception("Exception occured in TextCommand: " + exception.Message);
            }
        }
    }
}