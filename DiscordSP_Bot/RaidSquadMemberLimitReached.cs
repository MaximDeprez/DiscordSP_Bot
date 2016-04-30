using System;

namespace DiscordSP_Bot
{
	public class RaidSquadMemberLimitReached : Exception
	{
		public RaidSquadMemberLimitReached()
		{
		}

		public RaidSquadMemberLimitReached(string message) : base(message)
		{
		}

		public RaidSquadMemberLimitReached(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

