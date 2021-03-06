﻿using System;
using System.Text.RegularExpressions;

namespace GC.ToyRobot.Commands
{
	interface IBaseCommandFactory<T> where T : class
	{
		BaseCommand<T> GetCommand(T receiver, string commandText, Action<string> reportCallback);
	}

	class RobotCommandFactory : IBaseCommandFactory<IRobot>
	{
		private static RobotCommandFactory _instance = new RobotCommandFactory();
		private RobotCommandFactory() { }
		public static RobotCommandFactory Creator
		{
			get
			{
				return _instance;
			}
		}

		public BaseCommand<IRobot> GetCommand(IRobot receiver, string commandText, Action<string> reportCallback)
		{
			if (receiver == null) throw new ArgumentNullException(nameof(receiver));

			var placeMatch = Regex.Match(commandText, @"^PLACE (\d+),(\d+),(NORTH|EAST|SOUTH|WEST)$");
			if (placeMatch.Success)
				return new PlaceCommand(receiver, byte.Parse(placeMatch.Groups[1].Value), byte.Parse(placeMatch.Groups[2].Value), (Directions)Enum.Parse(typeof(Directions), placeMatch.Groups[3].Value));
			else
			{
				switch (commandText)
				{
					case "MOVE":
						return new MoveCommand(receiver);
					case "LEFT":
						return new LeftCommand(receiver);
					case "RIGHT":
						return new RightCommand(receiver);
					case "REPORT":
						return new ReportCommand(receiver, reportCallback);
				}
			}

			return null;
		}
	}
}
