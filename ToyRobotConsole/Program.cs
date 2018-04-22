using Autofac;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotConsole
{
	class Program
	{
		private static IContainer _diContainer;
		private static CommandLineOptions _opts = new CommandLineOptions();

		static void Main(string[] args)
		{			
			CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(() => _opts, args).WithNotParsed(HandleParseError);
			registerTypes();
			Run();
		}

		private static void HandleParseError(IEnumerable<Error> errs)
		{
			Console.ReadLine();
			Environment.Exit(0);
		}

		private static void Run()
		{
			var puppeteer = new GC.toyrobot.business.Puppeteer(_diContainer.Resolve<GC.toyrobot.business.IRobot>(), (r) => { Console.WriteLine(r); });
			if (!string.IsNullOrWhiteSpace(_opts.File))
			{
				//versione batch
				var commands = File.ReadAllLines(_opts.File);
				puppeteer.EnqueueCommands(commands.ToList());
				puppeteer.ExecuteQueue();
			}
			else
			{
				Console.WriteLine("Getting started with your Toy Robot, please write commands");
				var command = string.Empty;
				do
				{
					command = Console.ReadLine();
					puppeteer.ExecuteCommand(command);

				} while (command != "EXIT");
			}

			Console.ReadLine();
		}

		private static void registerTypes()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<GC.toyrobot.business.Robot>().As<GC.toyrobot.business.IRobot>().WithParameter("robotSpeed", _opts.RobotSpeed);
			builder.RegisterType<GC.toyrobot.business.Tabletop>().As<GC.toyrobot.business.IField>().WithParameter("tableSize", new Size(_opts.TableSize, _opts.TableSize));
			_diContainer = builder.Build();
		}
	}
}
