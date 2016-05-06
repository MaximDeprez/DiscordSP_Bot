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

		public string Identifier
		{
			get { return identifier; }
		}
			
		public RaidSquad()
		{
			DateTime now = DateTime.Now;

			this.identifier = "squad-" + now.Year + now.Month + now.Day;
		}

		public RaidSquad(string identifier)
		{
			this.identifier = identifier;
		}

		public bool Create(string username)
		{
			string path = "../../Resources/" + this.identifier + "__" + username + ".txt";

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

			return this.Join(username, username);
		}

		public void Disband(string username)
		{
			var path = "../../Resources/" + this.identifier + "__" + username + ".txt";

			if ( ! File.Exists(path))
			{
				throw new RaidSquadNotFoundException();
			}

			File.Delete(path);
		}

		public bool Join(string leader, string username)
		{
			string[] memberList =  this.GetMemberList(leader);

			if (memberList.Count() > 9)
			{
				throw new RaidSquadMemberLimitReached();
			}
				
			if (memberList.Contains(username))
			{
				throw new RaidSquadMemberAlreadyJoined();
			}

			this.AddUsernameToFile(leader, username);

			return true;
		}

		public string[] GetMemberList(string username)
		{
			var path = "../../Resources/" + this.identifier + "__" + username + ".txt";

			if ( ! File.Exists(path))
			{
				throw new RaidSquadNotFoundException();
			}

			return File.ReadLines(path).ToArray();
		}

		public void AddUsernameToFile(string leader, string username)
		{
			var path = "../../Resources/" + this.identifier + "__" + leader + ".txt";

			if ( ! File.Exists(path))
			{
				throw new RaidSquadNotFoundException();
			}

			using (StreamWriter sw = File.AppendText(path))
			{
				sw.WriteLine(username);
			}
		}

		public FileInfo[] GetSquadList()
		{
			DirectoryInfo info = new DirectoryInfo("../../Resources/");

			return info
				.GetFiles()
				.Where(f => f.Name.StartsWith("squad-"))
				.OrderByDescending(f => f.CreationTime)
				.ToArray();
		}

	}
}

