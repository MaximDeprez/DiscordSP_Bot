using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace DiscordSP_Bot
{
	public class RaidSquad
	{

		private string identifier;

		public string Identifier {
			get { return identifier; }
		}
			
		public RaidSquad()
		{
			DateTime now = DateTime.Now;

			this.identifier = "pveRaid-" + now.Year + now.Month + now.Day;
		}

		public RaidSquad(string identifier)
		{
			this.identifier = identifier;
		}

		public bool Create(string username)
		{
			string path = "../../Resources/" + this.identifier + ".txt";

			if (File.Exists(path))
			{
//				throw new RaidSquadAlreadyExistException(
//					"The raid squad with the given identifier [" + this.identifier + "] already exists."
//				);
				return false;
			}

			using (FileStream fs = File.Create(path))
			{
				
			}

			return this.Join(username);
		}

		public void Disband(string identifier)
		{
			File.Delete("../../Resources/" + identifier + ".txt");
		}

		public bool Join(string username)
		{
			if ( ! File.Exists("../../Resources/" + this.identifier + ".txt"))
			{
				throw new RaidSquadNotFoundException();
			}

			string[] memberList =  this.GetMemberList();

			if (memberList.Count() > 9)
			{
				throw new RaidSquadMemberLimitReached();
				return false;
			}
				
			if (memberList.Contains(username))
			{
				throw new RaidSquadMemberAlreadyJoined();
				return false;
			}

			this.AddUsernameToFile(username);

			return true;
		}

		public string[] GetMemberList()
		{
			return File.ReadLines("../../Resources/" + this.identifier + ".txt").ToArray();
		}

		public void AddUsernameToFile(string username)
		{
			using (StreamWriter sw = File.AppendText("../../Resources/" + this.identifier + ".txt"))
			{
				sw.WriteLine(username);
			}
		}

	}
}

