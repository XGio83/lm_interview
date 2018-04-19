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
		Queue<BaseCommand<Robot>> _robotCommandsQueue;
		Robot _robot;
		Tabletop _table;
		Action<string> _reportCallback;
		public Puppeteer(Size tabletopSize, byte robotSpeed, Action<string> reportCallback)
		{
			_table = new Tabletop(tabletopSize);
			_robot = new Robot(_table, robotSpeed);
			_robotCommandsQueue = new Queue<BaseCommand<Robot>>();
			_reportCallback = reportCallback;
		}
		internal Puppeteer(Size tabletopSize, byte robotSpeed, Action<string> reportCallback, List<BaseCommand<Robot>> commands): this(tabletopSize,robotSpeed,reportCallback)
		{
			_robotCommandsQueue = new Queue<BaseCommand<Robot>>(commands);
		}

		public void EnqueueCommand(string commandText)
		{
			var factory = new RobotCommandFactory(_robot);
			var command = factory.ParseCommand(commandText);
			if(command != null)
				_robotCommandsQueue.Enqueue(command);
		}

		public string ExecuteCommand(string commandText)
		{
			//esegui subito il command
			var factory = new RobotCommandFactory(_robot);
			var command = factory.ParseCommand(commandText);
			if (command != null)
			{
				command.Execute();
				return command.Result;
			}
			return string.Empty;
		}
		
		public void ExecuteQueue()
		{
			while (_robotCommandsQueue.Count > 0)
			{
				var command = _robotCommandsQueue.Dequeue();
				command.Execute();
				if (!string.IsNullOrWhiteSpace(command.Result) && _reportCallback != null) _reportCallback.Invoke(command.Result);
			}
		}
	}
}
