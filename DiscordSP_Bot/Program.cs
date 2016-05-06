﻿using DiscordSharp;
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

				if (e.MessageText == m_Commandsign + "squad")
                {
					RaidSquad sq = new RaidSquad();

					List<string> squadList = new List<string>();

					foreach (var name in sq.GetSquadList())
					{
						squadList.Add(name + ". Type `join @mention` to join.");
					}

					e.Channel.SendMessage("```" + squadList + "```");
                }

				if (e.MessageText == m_Commandsign + "squad create")
				{
					RaidSquad sq = new RaidSquad();

					string username = e.Author.Username + "#" + e.Author.Discriminator;

					sq.Create(username);

					e.Channel.SendMessage("Squad has been created. Type ´join @"+ username +"` to join the squad.");
				}

				if (e.MessageText == m_Commandsign + "squad disband")
				{
					RaidSquad sq = new RaidSquad();

					string username = e.Author.Username + "#" + e.Author.Discriminator;

					try
					{
						sq.Disband(username);
					}
					catch (RaidSquadNotFoundException exception)
					{
						e.Channel.SendMessage("Squad by by " + username + " cannot been found.");
					}
				}

				// @todo !join @Username (is a mention, I think)
				if (e.MessageText.StartsWith(m_Commandsign + "join"))
                {

                    dynamic rawJson = Newtonsoft.Json.JsonConvert.DeserializeObject(e.RawJson.ToString());

                    var mentions = rawJson.d.mentions;
                    foreach (var m in mentions)
                    {
                        Console.WriteLine(m.username);
                    }


//					Console.WriteLine(e.RawJson);
//Console.WriteLine(e.RawJson.ToObject<Array>()["d"]["mentions"][0]["id"]);

                    //                    string username = e.Author.Username + "#" + e.Author.Discriminator;
                    //
                    //					RaidSquad sq = new RaidSquad();
                    //                    bool a = sq.Join("mention", username);
                    //                    // string username = e.Author.Username + "#" + e.Author.Discriminator;
                    //                    // string message = JoinRaidSquad(username, m_ResourcePath, m_FilePrefix);
                    //					e.Channel.SendMessage(a ? "Joined" : "Not Joined");
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
//
//        static public string CreateRaidSquad(string username, string resourcePath, string filePrefix)
//        {
//            FileInfo[] files = GetPveRaidFiles(resourcePath, filePrefix);
//
//            string formattedDateNow = DateTime.Now.ToString("yyyy-MM-d");
//
//            if (files.Count() > 0)
//            {
//                // Pve Raid file already exists.
//                return "" +
//                    "A raid squad has been already created. People can join with `!join`.\n" +
//                    "```" + File.ReadAllText(resourcePath + filePrefix + formattedDateNow + ".txt") + "```";
//            }
//
//            StreamWriter sw = File.CreateText(
//                resourcePath + filePrefix + formattedDateNow + ".txt"
//            );
//
//            sw.WriteLine(username);
//            sw.Close();
//
//            return "The raid squad has been created! People can now join with ´!join´.";
//        }
//
//        static public string JoinRaidSquad(string username, string resourcePath, string filePrefix)
//        {
//            FileInfo[] files = GetPveRaidFiles(resourcePath, filePrefix);
//
//            if (files.Count() == 0)
//            {
//                return "There is not raid to join.";
//            }
//
//            FileInfo file = files.First();
//
//            int lineCount = 0;
//
//            foreach (string line in File.ReadLines(resourcePath + file.Name))
//            {
//                if (line.Equals(username))
//                {
//                    return "You've already joined."a;
//                }
//
//                lineCount++;
//            }
//
//            if (lineCount >= 10)
//            {
//                return "Sorry, ten people have already joined the raid.";
//            }
//
//            StreamWriter sw = new StreamWriter(resourcePath + file.Name, true);
//            sw.WriteLine(username);
//            sw.Close();
//
//            return ""+
//                "You have joined the raid squad!\n" +
//                "```" + File.ReadAllText(resourcePath + file.Name) + "```" +
//                (10 - lineCount - 1) + " spot(s) left.";
//        }
//
//        static public FileInfo GetPveRaidFile(string resourcePath, string filePrefix)
//        {
//			return new FileInfo(resourcePath + filePrefix);
//        }

    }
}
