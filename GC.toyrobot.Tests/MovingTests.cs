using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GC.toyrobot.business;
using GC.toyrobot.business.Commands;
using System.Diagnostics;

namespace GC.toyrobot.Tests
{
	[TestClass]
	public class MovingTests
	{
		private Robot _robot;
		private Tabletop _table;

		[TestInitialize]
		public void Init()
		{
			_table = new Tabletop(new System.Drawing.Size(5, 5));
			_robot = new Robot(_table);
		}

		[TestMethod]
		public void Movement_FromFile_NoCommands()
		{
			var reportedPosition = string.Empty;
			foreach (var command in getStringCommands())
			{
				var placeMatch = Regex.Match(command, @"^PLACE (\d+),(\d+),(NORTH|EAST|SOUTH|WEST)$");
				if (placeMatch.Success)
					_robot.Place(byte.Parse(placeMatch.Groups[1].Value), byte.Parse(placeMatch.Groups[2].Value), (Directions)Enum.Parse(typeof(Directions), placeMatch.Groups[3].Value));
				else
				{
					switch (command)
					{
						case "MOVE":
							_robot.Move();
							break;
						case "LEFT":
							_robot.CounterClockwiseTurn();
							break;
						case "RIGHT":
							_robot.ClockwiseTurn();
							break;
						case "REPORT":
							reportedPosition = _robot.ReportPosition();
							break;
					}
				}			
			}

			Assert.AreEqual("3,3,NORTH", reportedPosition);
		}

		[TestMethod]
		public void Commands_factory_commandParsing()
		{
			var commandList = new List<BaseCommand<Robot>>();
			foreach (var command in getStringCommands())
			{
				//FAI LA IROBOT. COSI DOPO PUOI CREARE QUI UN BUDDYROBOT A FINI DI TEST
				commandList.Add(RobotCommandFactory.Creator.GetCommand(new Robot(new Tabletop(new System.Drawing.Size(0,0))),command, null));
			}
			Assert.IsTrue(commandList.Count == 9);
			Assert.IsInstanceOfType(commandList[0], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[1], typeof(LeftCommand));
			Assert.IsInstanceOfType(commandList[2], typeof(PlaceCommand));
			Assert.IsInstanceOfType(commandList[3], typeof(PlaceCommand));
			Assert.IsInstanceOfType(commandList[4], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[5], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[6], typeof(LeftCommand));
			Assert.IsInstanceOfType(commandList[7], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[8], typeof(ReportCommand));
		}

		[TestMethod]
		public void Commands_puppeteer_enqueue()
		{
			Action<string> reportCallback = (string rep) => {
				Debug.WriteLine(rep);
				Assert.AreEqual("3,3,NORTH", rep);
			};
			var puppeteer = new Puppeteer(new System.Drawing.Size(5, 5), 1, reportCallback);
			puppeteer.EnqueueCommands(getStringCommands().ToList());			
			puppeteer.ExecuteQueue();
		}


		private IEnumerable<string> getStringCommands()
		{
			return File.ReadAllLines("TestData/Data1.txt", System.Text.Encoding.UTF8).Select(l => l.Trim());
		}
		
	}
}
