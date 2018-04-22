using Autofac;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GC.ToyRobot.Console
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
			System.Console.ReadLine();
			Environment.Exit(0);
		}

		private static void Run()
		{
			var puppeteer = new Puppeteer(_diContainer.Resolve<IRobot>(), (r) => { System.Console.WriteLine(r); });
			if (!string.IsNullOrWhiteSpace(_opts.File))
			{
				//versione batch
				if (File.Exists(_opts.File))
				{
					var commands = File.ReadAllLines(_opts.File);
					puppeteer.EnqueueCommands(commands.ToList());
					puppeteer.ExecuteQueue();
				}
				else
				{
					System.Console.WriteLine("The file provided does not exists");
				}
			}
			else
			{
				System.Console.WriteLine("Getting started with your Toy Robot, please write commands:");
				var command = string.Empty;
				do
				{
					command = System.Console.ReadLine();
					puppeteer.ExecuteCommand(command);

				} while (command != "EXIT");
			}

			System.Console.ReadLine();
		}

		private static void registerTypes()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<Robot>().As<IRobot>().WithParameter("robotSpeed", _opts.RobotSpeed);
			builder.RegisterType<Tabletop>().As<IField>().WithParameter("tableSize", new Size(_opts.TableSize, _opts.TableSize));
			_diContainer = builder.Build();
		}
	}
}
