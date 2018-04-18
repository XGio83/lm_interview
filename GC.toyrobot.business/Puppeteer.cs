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
	class Puppeteer //invoker facade
	{
		List<BaseCommand<Robot>> _robotCommands;
		Robot _robot;
		Tabletop _table;
		public Puppeteer(Size tabletopSize, byte robotSpeed)
		{
			_table = new Tabletop(tabletopSize);
			_robot = new Robot(_table, robotSpeed);
			_robotCommands = new List<BaseCommand<Robot>>();
		}
		public Puppeteer(Size tabletopSize, byte robotSpeed, List<BaseCommand<Robot>> commands): this(tabletopSize,robotSpeed)
		{
			_robotCommands.AddRange(commands);
		}

		//add command?
		//command factory?
		//run commands
		//parsing dei comandi per aggiungerli nella lista
		//command buffer eseguito al momento del report? perchè in caso di console application e interazione diretta dell'utente probabilmente vorranno che il command sia eseguito subito
		//pensa a una valenza doppia del puppeteer, con ExecuteCommand(esegue subito) e AddCommand ed ExecuteCommands

		public void AddCommand(string commandText)
		{
			var factory = new RobotCommandFactory(_robot);
			_robotCommands.Add(factory.ParseCommand(commandText));
		}

		public void ExeecuteCommand(BaseCommand<Robot> command)
		{
			//esegui subito il command

		}
		public void MoveRobot()
		{
			_robotCommands.ForEach(c => c.Execute());
		}


	}
}
