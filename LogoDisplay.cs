using System;
using System.Drawing;
using System.IO;

namespace CybersecurityChatbot
{
    public class LogoDisplay
    {
        public static void DisplayLogo()
        {
            string imagePath = "logo.jpg"; // Relative path

            try
            {
                if (File.Exists(imagePath))
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                  
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error loading image: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
