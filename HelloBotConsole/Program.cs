using System;
using Telegram.Bot;

namespace HelloBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("760620360:AAGq9KIBMccVIGo0Rbggq3Hn88ehgLVNq38");
            var me = botClient.GetMeAsync().Result;

            Console.WriteLine($"Hello World I'm the:  ID:{me.Id} / FirstName:{me.FirstName} / Username:{me.Username}!");
        }
    }
}