﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GC.toyrobot.business;
using GC.toyrobot.business.Commands;

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
			var possibleCommands = File.ReadAllLines("TestData/Data1.txt");
			var factory = new RobotCommandFactory(_robot);
			var commandList = new List<BaseCommand<Robot>>();
			foreach (var command in getStringCommands())
			{
				commandList.Add(factory.ParseCommand(command));
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


		private IEnumerable<string> getStringCommands()
		{
			return File.ReadAllLines("TestData/Data1.txt", System.Text.Encoding.UTF8).Select(l => l.Trim());
		}
		
	}
}