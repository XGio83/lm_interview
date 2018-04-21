using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	interface ICommandFactory<T>
	{
		BaseCommand<T> GetCommand(T receiver, string commandText, Action<string> reportCallback);
	}

	class RobotCommandFactory : ICommandFactory<Robot>
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

		public BaseCommand<Robot> GetCommand(Robot receiver, string commandText, Action<string> reportCallback)
		{
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
