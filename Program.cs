using System;
using System.Drawing;
using System.IO;
using NAudio.Wave; // For sound playback
using System.Threading;

class Program
{
    static void Main()
    {
        // Play the audio greeting
        PlayVoiceGreeting();

        // Show welcome message and logo
        DisplayWelcomeMessage();
        DisplayAsciiArt();

        // Start chatbot interaction
        StartChatbot();
        ChatbotResponses();
    }

    // Method to play voice greeting from a .wav file
    static void PlayVoiceGreeting()
    {
        try
        {
            string audioPath = @"C:\Users\RC_Student_lab\Desktop\emily monegi\poe part1 prog\bin\Debug\net8.0\greeting.wav";

            using (var audioFile = new WaveFileReader(audioPath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Wait until playback finishes
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error playing audio: " + ex.Message);
            Console.ResetColor();
        }
    }

    // Method to display the ASCII art logo from image
    static void DisplayAsciiArt()
    {
        string imagePath = @"C:\Users\RC_Student_lab\Desktop\emily monegi\poe part1 prog\bin\Debug\net8.0\logo.jpg";

        try
        {
            using (Bitmap logo = new Bitmap(imagePath))
            {
                int newWidth = 100;
                int newHeight = (int)(logo.Height * newWidth / logo.Width * 0.5);

                using (Bitmap resizedLogo = new Bitmap(logo, new Size(newWidth, newHeight)))
                {
                    string asciiChars = " .:-=+*%@#";

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    for (int y = 0; y < resizedLogo.Height; y++)
                    {
                        for (int x = 0; x < resizedLogo.Width; x++)
                        {
                            Color pixelColor = resizedLogo.GetPixel(x, y);
                            int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                            char asciiChar = asciiChars[gray * (asciiChars.Length - 1) / 255];
                            Console.Write(asciiChar);
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading image: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Method to show welcome message
    static void DisplayWelcomeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("         WELCOME TO THE CYBERSECURITY AWARENESS BOT ");
        Console.ResetColor();
    }

    // Ask user for their name and greet them
    static void StartChatbot()
    {
        Console.Write("\nEnter your name: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Name cannot be empty. Please enter your name: ");
            Console.ResetColor();
            name = Console.ReadLine();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nHello, {name}! Welcome to your Cybersecurity Awareness Bot.");
        Console.WriteLine("You can ask me about cybersecurity topics like passwords, phishing, and safe browsing.");
        Console.ResetColor();
    }

    // Main chatbot response logic using an array for questions
    static void ChatbotResponses()
    {
        // Array of predefined questions
        string[] questions = {
            "how are you?",
            "what's your purpose?",
            "what can i ask you about?",
            "password safety",
            "phishing",
            "safe browsing",
            "exit"
        };

        // Corresponding answers
        string[] answers = {
            "I'm a bot, so I don't have feelings, but I'm here to help!",
            "I provide cybersecurity tips to keep you safe online.",
            "You can ask me about password safety, phishing, and safe browsing.",
            "Use strong passwords with a mix of uppercase, lowercase, numbers, and symbols. Avoid using personal details.",
            "Be cautious of emails asking for personal information. Verify links before clicking.",
            "Keep your software updated, avoid suspicious websites, and use antivirus protection.",
            "Goodbye! Stay safe online."
        };

        while (true)
        {
            Console.Write("\nAsk me a question: ");
            string question = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(question))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid question.");
                Console.ResetColor();
                continue;
            }

            bool found = false;

            for (int i = 0; i < questions.Length; i++)
            {
                if (question == questions[i])
                {
                    if (questions[i] == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypingEffect(answers[i]);
                        Console.ResetColor();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    TypingEffect(answers[i]);
                    Console.ResetColor();
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypingEffect("I didn't quite understand that. Could you rephrase?");
                Console.ResetColor();
            }
        }
    }

    // Typing animation effect in console
    static void TypingEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }
        Console.WriteLine();
    }
}