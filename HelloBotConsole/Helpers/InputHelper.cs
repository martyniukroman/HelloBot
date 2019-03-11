using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace HelloBotConsole.Helpers
{
    public static class InputHelper
    {
        private static bool IsWaitingForNextValue = true;

        private static ITelegramBotClient botClient =
            new TelegramBotClient("760620360:AAGq9KIBMccVIGo0Rbggq3Hn88ehgLVNq38");

        public static async Task<string> Insert(string Message, MessageEventArgs e)
        {
//            if (e.Message.Text.Contains("/"))
//            {
//                throw new Exception("Your name Contains \'/\'");
//            }

            if (IsWaitingForNextValue)
            {
                await botClient.SendTextMessageAsync(e.Message.Chat, Message);
                IsWaitingForNextValue = !IsWaitingForNextValue;
            }
                return e.Message.Text;
        }
    }
}