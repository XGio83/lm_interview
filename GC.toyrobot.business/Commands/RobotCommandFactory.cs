using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class RobotCommandFactory
	{
		private Robot _robot;
		private Action<string> _reportCallback = null;
		private RobotCommandFactory() { }
		public RobotCommandFactory(Robot robot)
		{
			_robot = robot;
		}

		public RobotCommandFactory(Robot robot, Action<string> reportCallback) : this(robot)
		{
			_reportCallback = reportCallback;
		}

		public BaseCommand<Robot> ParseCommand(string commandText)
		{
			var placeMatch = Regex.Match(commandText, @"^PLACE (\d+),(\d+),(NORTH|EAST|SOUTH|WEST)$");
			if (placeMatch.Success)
				return new PlaceCommand(_robot, byte.Parse(placeMatch.Groups[1].Value), byte.Parse(placeMatch.Groups[2].Value), (Directions)Enum.Parse(typeof(Directions), placeMatch.Groups[3].Value));
			else
			{
				switch (commandText)
				{
					case "MOVE":
						return new MoveCommand(_robot);
					case "LEFT":
						return new LeftCommand(_robot);
					case "RIGHT":
						return new RightCommand(_robot);
					case "REPORT":
						return new ReportCommand(_robot, _reportCallback);
				}
			}

			return null;
		}
	}
}
