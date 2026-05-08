using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public class ConsoleHelper
    {
        public static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                WELCOME TO THE CYBERSECURITY AWARENESS BOT ");
            Console.ResetColor();
        }

        public static void TypingEffect(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
