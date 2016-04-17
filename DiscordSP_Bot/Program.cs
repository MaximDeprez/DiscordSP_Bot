using DiscordSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;



namespace DiscordSP_Bot
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Defining variables");
            DiscordClient client = new DiscordClient("MTcwODkzNTEzNTk1MTU4NTI4.CfP75Q.kyPcmCMumkrwFenTdKutEeZnFhM", true);

            string m_Commandsign = "!";

            client.Connected += (sender, e) =>
            {
                Console.WriteLine("Connected! User: " + e.User.Username);

                client.UpdateCurrentGame("At your service!");
            };

            client.MessageReceived += (sender, e) => // Channel message has been received
            {
                if (e.MessageText == m_Commandsign + "info")
                {
                    e.Channel.SendMessage("```" + File.ReadAllText("../../Resources/info.txt") + "```");
                }

                if (e.MessageText == m_Commandsign + "schedule")
                {
                    e.Channel.SendMessage("```" + File.ReadAllText("../../Resources/schedule.txt") + "```");
                }

                if (e.MessageText == m_Commandsign + "commands")
                {
                    e.Channel.SendMessage("```" + File.ReadAllText("../../Resources/commands.txt") + "```");
                }

                if (e.MessageText == m_Commandsign + "website")
                {
                    e.Channel.SendMessage("http://shadowprisoners.org/");
                }
            };

            try
            {
                // Make sure that IF something goes wrong, the user will be notified.
                // The SendLoginRequest should be called after the events are defined, to prevent issues.
                Console.WriteLine("Sending login request");
                client.SendLoginRequest();
                Console.WriteLine("Connecting client in separate thread");
                Thread connect = new Thread(client.Connect);
                connect.Start();
                // Login request, and then connect using the discordclient i just made.
                Console.WriteLine("Client connected!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong!\n" + e.Message + "\nPress any key to close this window.");
            }

            Console.ReadKey();
            Environment.Exit(0);

        }
    }
}
