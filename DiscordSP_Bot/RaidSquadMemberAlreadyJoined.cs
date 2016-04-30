using System;

namespace DiscordSP_Bot
{
	public class RaidSquadMemberAlreadyJoined : Exception
	{
		public RaidSquadMemberAlreadyJoined()
		{
		}

		public RaidSquadMemberAlreadyJoined(string message) : base(message)
		{
		}

		public RaidSquadMemberAlreadyJoined(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

