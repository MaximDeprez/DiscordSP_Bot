using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DiscordSP_Bot
{
	public class RaidSquadTest
	{

		private RaidSquad sq;

		public RaidSquadTest()
		{
			this.sq = new RaidSquad();
		}

		[Test]
		public void test_the_raid_file_is_created()
		{
			try
			{
				sq.Create("Username#1234");

				Assert.True(File.Exists("../../Resources/" + sq.Identifier + "__" + "Username#1234" + ".txt"));	
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		public void test_the_raid_file_has_the_creator_name_in_first_line()
		{
			try
			{
				sq.Create("Username#1234");

				string line = File.ReadAllLines("../../Resources/" + sq.Identifier + "__" + "Username#1234" + ".txt")[0];

				Assert.True(String.Equals("Username#1234", line));
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		public void test_the_user_can_join_the_squad()
		{
			try
			{
				sq.Create("Username#1234");
				sq.Join("Username#1234", "TheOtherUser#1234");

				string[] memberList = sq.GetMemberList("Username#1234");

				Assert.False(String.Equals("TheOtherUser#1234", memberList[0]));
				Assert.True(String.Equals("TheOtherUser#1234", memberList[1]));
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		[ExpectedException(typeof(RaidSquadMemberLimitReached))]
		public void test_that_a_maximum_of_10_users_can_join()
		{
			try
			{
				sq.Create("Username#1234");

				for (int i = 0; i < 10; i++)
				{
					sq.Join("Username#1234", "TheOtherUser#123" + i);
				}
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		public void test_the_member_list_contains_the_usernames()
		{
			try
			{
				List<string> memberList = new List<string>();

				sq.Create("Username#1234");
				memberList.Add("Username#1234");

				for (int i = 0; i < 3; i++)
				{
					memberList.Add("TheOtherUser#123" + i);
					sq.Join("Username#1234", "TheOtherUser#123" + i);
				}

				string[] memberListFromSquad = sq.GetMemberList("Username#1234");

				for (int i = 0; i < memberList.Count; i++)
				{
					Assert.True(
						String.Equals(memberList[i], memberListFromSquad[i])
					);
				}
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		[ExpectedException(typeof(RaidSquadMemberAlreadyJoined))]
		public void test_the_user_can_join_only_once()
		{
			try
			{
				sq.Create("Username#1234");

				sq.Join("Username#1234", "Username#1234");
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

		[Test]
		[ExpectedException(typeof(RaidSquadNotFoundException))]
		public void test_the_user_cannot_join_if_no_squad_has_been_created()
		{
			try
			{
				sq.Join("Username#1234", "Username#1234");
			}
			finally
			{
				sq.Disband("Username#1234");
			}
		}

	}
}

