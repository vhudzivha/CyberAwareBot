using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using System.Threading;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private Dictionary<string, string> userMemory = new Dictionary<string, string>();
        private string currentTopic = "";
        private List<string> conversationHistory = new List<string>();

        private Dictionary<string, string[]> generalResponses = new Dictionary<string, string[]>
        {
            { "how are you?", new string[] { "I'm a bot, so I don't have feelings, but I'm here to help!", "I'm functioning optimally.", "Ready to assist you!" } },
            { "what's your purpose?", new string[] { "I provide cybersecurity tips to keep you safe online.", "My purpose is to raise awareness about cybersecurity threats and best practices.", "I'm here to educate you on how to protect yourself in the digital world." } },
            { "what can i ask you about?", new string[] { "You can ask me about password safety, phishing, safe browsing, malware, social engineering, and more.", "I can provide information on various cybersecurity topics.", "Feel free to ask me anything related to online security." } },
            { "exit", new string[] { "Goodbye! Stay safe online.", "Thank you for chatting. Be secure!", "Have a safe digital experience!" } }
        };


        private Dictionary<string, string[]> cybersecurityResponses = new Dictionary<string, string[]>
{
    {
        "password", new string[] {
            "Make sure to use strong, unique passwords for each account.",
            "A strong password should include uppercase and lowercase letters, numbers, and symbols.",
            "Avoid using personal details in your passwords.",
            "Consider using a password manager to securely store passwords.",
            "Enable two-factor authentication whenever possible."
        }
    },

    {
        "phishing", new string[] {
            "Be cautious of emails or messages asking for personal information.",
            "Never click suspicious links from unknown senders.",
            "Verify the sender before opening attachments.",
            "Phishing attacks often pretend to be trusted companies.",
            "Hover over links to see the real website address."
        }
    },

    {
        "safe browsing", new string[] {
            "Keep your browser updated.",
            "Only visit trusted websites.",
            "Look for HTTPS and the padlock icon in websites.",
            "Avoid downloading files from suspicious websites.",
            "Use antivirus software to stay protected."
        }
    },

    {
        "malware", new string[] {
            "Malware is harmful software that can damage your system.",
            "Install antivirus software and keep it updated.",
            "Avoid downloading software from unofficial websites.",
            "Be careful when opening email attachments.",
            "Scan your computer regularly for malware."
        }
    },

    {
        "ransomware", new string[] {
            "Ransomware locks your files and demands payment.",
            "Always keep backups of important files.",
            "Avoid suspicious email attachments.",
            "Keep your operating system updated.",
            "Never download unknown software."
        }
    },

    {
        "vpn", new string[] {
            "A VPN helps protect your online privacy.",
            "VPNs encrypt your internet connection.",
            "Using a VPN is useful on public Wi-Fi.",
            "Choose trusted VPN providers only."
        }
    },

    {
        "2fa", new string[] {
            "Two-Factor Authentication adds extra security.",
            "2FA requires a password and a second verification step.",
            "Enable 2FA on important accounts.",
            "2FA helps protect accounts even if passwords are stolen."
        }
    },

    {
        "social engineering", new string[] {
            "Social engineering tricks people into revealing information.",
            "Attackers may pretend to be trusted people.",
            "Never share passwords with strangers.",
            "Always verify identities before giving sensitive information.",
            "Be careful of urgent requests asking for private details."
        }
    }
};


        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"
  ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
 ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
 ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
 ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
 ╚██████╗   ██║   ██████╔╝███████╗██║  ██║
  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝

      CYBERSECURITY AWARENESS BOT
");

            Console.ResetColor();

            Console.Write("\nEnter your name: ");
            string name = Console.ReadLine();


            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Name cannot be empty. Please enter your name: ");
                Console.ResetColor();

                name = Console.ReadLine();
            }

            userMemory["name"] = name;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"\nHello, {name}! Welcome to your Cybersecurity Awareness Bot.");

            Console.WriteLine("You can ask me about cybersecurity topics like passwords, phishing, safe browsing, malware, social engineering, and more.");

            Console.ResetColor();

            Console.Write("\nWhat cybersecurity topic are you most interested in? ");

            string favoriteTopic = Console.ReadLine().ToLower();

            if (!string.IsNullOrWhiteSpace(favoriteTopic))
            {
                userMemory["favoriteTopic"] = favoriteTopic;

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"\nGreat! I'll remember that you're interested in {favoriteTopic}.");

                Console.ResetColor();
            }

            RespondToUser();
        }

        private void RespondToUser()
        {
            while (true)
            {
                Console.Write("\nAsk me a question: ");
                string question = Console.ReadLine().ToLower().Trim();
                conversationHistory.Add($"User: {question}");

                if (string.IsNullOrWhiteSpace(question))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (question.Contains("history"))  // Modified condition to check if the question contains "history"
                {
                    DisplayConversationHistory();
                    continue;
                }

                bool found = false;
                foreach (var keywordResponsePair in cybersecurityResponses)
                {
                    if (question.Contains(keywordResponsePair.Key))
                    {
                        currentTopic = keywordResponsePair.Key;
                        Random random = new Random();
                        string response = keywordResponsePair.Value[random.Next(keywordResponsePair.Value.Length)];
                        ConsoleHelper.TypingEffect(response);
                        conversationHistory.Add($"Bot: {response}");
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    foreach (var generalResponsePair in generalResponses)
                    {
                        if (question == generalResponsePair.Key)
                        {
                            currentTopic = generalResponsePair.Key;
                            Random random = new Random();
                            string response = generalResponsePair.Value[random.Next(generalResponsePair.Value.Length)];
                            ConsoleHelper.TypingEffect(response);
                            conversationHistory.Add($"Bot: {response}");
                            found = true;
                            if (generalResponsePair.Key == "exit")
                            {
                                SaveConversation();
                                return;
                            }
                            break;
                        }
                    }
                }
                if (!found && !string.IsNullOrEmpty(currentTopic))
                {
                    if (question.Contains("more") || question.Contains("details") || question.Contains("explain"))
                    {
                        string additionalInfo = "";
                        switch (currentTopic)
                        {
                            case "password":
                                additionalInfo = "For more details on password safety, consider using a passphrase, which is a long sentence that's easy to remember but hard to guess. Also, enable multi-factor authentication whenever possible for an extra layer of security.";
                                break;
                            case "phishing":
                                additionalInfo = "To further protect yourself from phishing, be extremely cautious of any communication that creates a sense of urgency or requires immediate action. Always verify requests through official and independent channels.";
                                break;
                            case "safe browsing":
                                additionalInfo = "When browsing safely, ensure your firewall is enabled and configured correctly. Be wary of accepting security certificates from untrusted sources.";
                                break;
                            case "malware":
                                additionalInfo = "To prevent malware infections, be careful about the software you install, even if it seems legitimate. Read reviews and download from official sources only.";
                                break;
                            case "social engineering":
                                additionalInfo = "Remember that social engineers often exploit human emotions like fear or greed. Take your time to think before acting on any unusual request.";
                                break;
                            default:
                                break;
                        }
                        ConsoleHelper.TypingEffect(additionalInfo);
                        conversationHistory.Add($"Bot: {additionalInfo}");
                        found = true;
                    }
                }
                if (!found && question.Contains("remember") && userMemory.ContainsKey("name"))
                {
                    string memoryResponse = $"Yes, {userMemory["name"]}, I remember you!";
                    ConsoleHelper.TypingEffect(memoryResponse);
                    conversationHistory.Add($"Bot: {memoryResponse}");
                    found = true;
                }
                if (!found && question.Contains("interested in") && userMemory.ContainsKey("favoriteTopic"))
                {
                    string favoriteTopicResponse = $"Since you are interested in {userMemory["favoriteTopic"]}, here's another tip related to it...";
                    ConsoleHelper.TypingEffect(favoriteTopicResponse);
                    conversationHistory.Add($"Bot: {favoriteTopicResponse}");
                    switch (userMemory["favoriteTopic"])
                    {
                        case "password":
                            ConsoleHelper.TypingEffect("Consider using a password strength checker tool online to evaluate the robustness of your passwords.");
                            conversationHistory.Add($"Bot: Consider using a password strength checker tool online to evaluate the robustness of your passwords.");
                            break;
                        case "phishing":
                            ConsoleHelper.TypingEffect("Be aware that phishing attempts can also occur via SMS (smishing) or phone calls (vishing).");
                            conversationHistory.Add($"Bot: Be aware that phishing attempts can also occur via SMS (smishing) or phone calls (vishing).");
                            break;
                        case "safe browsing":
                            ConsoleHelper.TypingEffect("Regularly clear your browsing history, cookies, and cache to protect your privacy.");
                            conversationHistory.Add($"Bot: Regularly clear your browsing history, cookies, and cache to protect your privacy.");
                            break;
                        case "malware":
                            ConsoleHelper.TypingEffect("Enable automatic updates for your operating system and applications to patch security vulnerabilities.");
                            conversationHistory.Add($"Bot: Enable automatic updates for your operating system and applications to patch security vulnerabilities.");
                            break;
                        case "social engineering":
                            ConsoleHelper.TypingEffect("Be cautious of sharing too much personal information on social media platforms, as this can be used for social engineering attacks.");
                            conversationHistory.Add($"Bot: Be cautious of sharing too much personal information on social media platforms.");
                            break;
                        default:
                            ConsoleHelper.TypingEffect("That's an interesting topic!");
                            conversationHistory.Add($"Bot: That's an interesting topic!");
                            break;
                    }
                    found = true;
                }
                if (!found && (question.Contains("worried") || question.Contains("concerned") || question.Contains("anxious")))
                {
                    string empathyResponse = "It's completely understandable to feel that way. Cybersecurity can seem overwhelming, but I'm here to help you understand and stay safe. What specific concerns do you have?";
                    ConsoleHelper.TypingEffect(empathyResponse);
                    conversationHistory.Add($"Bot: {empathyResponse}");
                    found = true;
                }
                else if (!found && (question.Contains("curious") || question.Contains("learn more") || question.Contains("tell me more")))
                {
                    string curiosityResponse = "That's great that you're curious! Learning about cybersecurity is the first step to staying protected. What specifically are you interested in exploring further?";
                    ConsoleHelper.TypingEffect(curiosityResponse);
                    conversationHistory.Add($"Bot: {curiosityResponse}");
                    found = true;
                }
                else if (!found && (question.Contains("frustrated") || question.Contains("confused") || question.Contains("difficult")))
                {
                    string frustrationResponse = "I understand it can be frustrating. Let's take it one step at a time. What part are you finding difficult? I'll try to explain it more clearly or provide a different perspective.";
                    ConsoleHelper.TypingEffect(frustrationResponse);
                    conversationHistory.Add($"Bot: {frustrationResponse}");
                    found = true;
                }
                if (!found)
                {
                    string unknownResponse = "I'm not sure I understand. Could you try rephrasing your question or asking about passwords, phishing, safe browsing, malware, or social engineering?";
                    ConsoleHelper.TypingEffect(unknownResponse);
                    conversationHistory.Add($"Bot: {unknownResponse}");
                }
            }
        }

        private void SaveConversation()
        {
            try
            {
                string filePath = "conversation.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string line in conversationHistory)
                    {
                        writer.WriteLine(line);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nConversation saved to {filePath}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error saving conversation: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void DisplayConversationHistory()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n--- Conversation History ---");
            foreach (string line in conversationHistory)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("--- End of History ---");
            Console.ResetColor();
        }
    }
}