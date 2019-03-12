using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace HelloBotConsole.Helpers
{
    public static class InputHelper
    {
        private static bool IsWaitingForNextValue = true;

        public static async Task<string> Insert(ITelegramBotClient botClient, string message, MessageEventArgs e)
        {
            IsWaitingForNextValue = !IsWaitingForNextValue;

            if (!IsWaitingForNextValue)
            {
                await botClient.SendTextMessageAsync(e.Message.Chat, message, ParseMode.Markdown);
                return null;
            }

            if (IsWaitingForNextValue)
            {
                return e.Message.Text;
            }

            return null;
        }
    }
}