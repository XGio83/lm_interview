using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.toyrobot.business;
using GC.toyrobot.business.Commands;

namespace GC.toyrobot.business
{
	public class Puppeteer //invoker facade
	{
		Queue<BaseCommand<IRobot>> _robotCommandsQueue;
		IRobot _robot;
		IField _table;
		Action<string> _reportCallback;
		public Puppeteer(Size tabletopSize, byte robotSpeed, Action<string> reportCallback)
		{
			_table = new Tabletop(tabletopSize);
			_robot = new Robot(_table, robotSpeed);
			_robotCommandsQueue = new Queue<BaseCommand<IRobot>>();
			_reportCallback = reportCallback;
		}
		internal Puppeteer(Size tabletopSize, byte robotSpeed, Action<string> reportCallback, List<BaseCommand<IRobot>> commands): this(tabletopSize,robotSpeed,reportCallback)
		{
			_robotCommandsQueue = new Queue<BaseCommand<IRobot>>(commands);
		}

		public void EnqueueCommands(List<string> commandsText)
		{
			commandsText.ForEach(c => {
				var command = RobotCommandFactory.Creator.GetCommand(_robot, c, _reportCallback);
				if (command != null)
					_robotCommandsQueue.Enqueue(command);
			});
		}

		public string ExecuteCommand(string commandText)
		{
			//esegui subito il command
			var command = RobotCommandFactory.Creator.GetCommand(_robot, commandText, _reportCallback);
			if (command != null)
			{
				command.Execute();
			}
			return string.Empty;
		}
		
		public void ExecuteQueue()
		{
			while (_robotCommandsQueue.Count > 0)
			{
				var command = _robotCommandsQueue.Dequeue();
				command.Execute();
			}
		}
	}
}
