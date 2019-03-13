using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloBotConsole.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace HelloBotConsole.Helpers
{
    public static class InputHelper
    {

        public static async Task<string> Insert(ITelegramBotClient botClient, string message, Session session, MessageEventArgs e)
        {
            if (!e.Message.Text.Contains("/"))
            {
                return e.Message.Text;
            }
                return null;        
        }
    }
}