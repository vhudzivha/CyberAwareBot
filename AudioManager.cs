using System.Media;
using System;
using System.Media;
using System.IO;

namespace CybersecurityChatbot
{
    public class AudioManager
    {
        public static void PlayGreeting()
        {
            try
            {
                string audioPath = "greeting.wav";

                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.PlaySync();
                }
                else
                {
                    Console.WriteLine("Greeting audio file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing audio: " + ex.Message);
            }
        }
    }
}