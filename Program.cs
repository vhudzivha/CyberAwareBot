using System;

namespace CybersecurityChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.DisplayWelcomeMessage();
            LogoDisplay.DisplayLogo();

            Chatbot bot = new Chatbot();
            bot.Start();
        }
    }
}