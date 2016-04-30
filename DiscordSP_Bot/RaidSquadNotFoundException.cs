using System;

namespace DiscordSP_Bot
{
	public class RaidSquadNotFoundException: Exception
	{
		public RaidSquadNotFoundException()
		{
		}

		public RaidSquadNotFoundException(string message) : base(message)
		{
		}

		public RaidSquadNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

