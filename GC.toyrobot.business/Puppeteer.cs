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
		Action<string> _reportCallback;

		public Puppeteer(IRobot robot, Action<string> reportCallback)
		{
			_robot = robot ?? throw new ArgumentNullException(nameof(robot));
			_robotCommandsQueue = new Queue<BaseCommand<IRobot>>();
			_reportCallback = reportCallback;
		}

		internal Puppeteer(IRobot robot, Action<string> reportCallback, List<BaseCommand<IRobot>> commands) : this(robot, reportCallback)
		{
			if (commands == null) throw new ArgumentNullException(nameof(commands));

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
