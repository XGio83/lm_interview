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
		public Puppeteer(Size tabletopSize, byte robotSpeed)
		{
			_table = new Tabletop(tabletopSize);
			_robot = new Robot(_table, robotSpeed);
			_robotCommandsQueue = new Queue<BaseCommand<Robot>>();
		}
		internal Puppeteer(Size tabletopSize, byte robotSpeed, List<BaseCommand<Robot>> commands): this(tabletopSize,robotSpeed)
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

		public void ExecuteCommand(string commandText)
		{
			//esegui subito il command
			var factory = new RobotCommandFactory(_robot);
			var command = factory.ParseCommand(commandText);
			if(command != null) command.Execute();
		}

		public void ExecuteQueue()
		{
			while (_robotCommandsQueue.Count > 0)
			{
				_robotCommandsQueue.Dequeue().Execute();
			}
		}
	}
}
