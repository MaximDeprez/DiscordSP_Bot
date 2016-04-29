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

				Assert.True(File.Exists("../../Resources/" + sq.Identifier + ".txt"));	
			}
			finally
			{
				sq.Disband(sq.Identifier);
			}
		}

		[Test]
		public void test_the_raid_file_has_the_creator_name_in_first_line()
		{
			try
			{
				sq.Create("Username#1234");

				string line = File.ReadAllLines("../../Resources/" + sq.Identifier + ".txt")[0];

				Assert.True(String.Equals("Username#1234", line));
			}
			finally
			{
				sq.Disband(sq.Identifier);
			}
		}

		[Test]
		public void test_that_a_user_can_join_the_squad()
		{
			try
			{
				sq.Create("Username#1234");
				sq.Join("TheOtherUser#1234");

				string[] lineList = File.ReadAllLines("../../Resources/" + sq.Identifier + ".txt");

				Assert.False(String.Equals("TheOtherUser#1234", lineList[0]));
				Assert.True(String.Equals("TheOtherUser#1234", lineList[1]));
			}
			finally
			{
				sq.Disband(sq.Identifier);
			}
		}

//		[Test]
//		public void test_that_a_maximum_of_10_users_can_join()
//		{
//			try
//			{
//				bool didJoin = true;
//
//				sq.Create("Username#1234");
//
//				for (int i = 0; i < 11; i++)
//				{
//					didJoin = sq.Join("TheOtherUser#123" + i);
//				}
//
//				Assert.False(didJoin);
//			}
//			finally
//			{
//				sq.Disband(sq.Identifier);
//			}
//		}

		[Test]
		public void test_that_member_list_contains_the_usernames()
		{
			try
			{
				List<string> memberList = new List<string>();

				sq.Create("Username#1234");
				memberList.Add("Username#1234");

				for (int i = 0; i < 3; i++)
				{
					memberList.Add("TheOtherUser#123" + i);
					sq.Join("TheOtherUser#123" + i);
				}

				string[] memberListFromSquad = sq.GetMemberList();

				for (int i = 0; i < memberList.Count; i++)
				{
					Assert.True(
						String.Equals(memberList[i], memberListFromSquad[i])
					);
				}
			}
			finally
			{
				sq.Disband(sq.Identifier);
			}
		}

	}
}

