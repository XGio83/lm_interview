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
		static void Main(string[] args)
		{			
				CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
					.WithParsed<CommandLineOptions>(opts => Run(opts))
					.WithNotParsed<CommandLineOptions>((errs) => HandleParseError(errs));
		}

		private static void HandleParseError(IEnumerable<Error> errs)
		{
			Console.ReadLine();
		}

		private static void Run(CommandLineOptions opts)
		{
			var puppeteer = new GC.toyrobot.business.Puppeteer(new Size(opts.TableSize, opts.TableSize), opts.RobotSpeed, (r) => { Console.WriteLine(r); });
			if (!string.IsNullOrWhiteSpace(opts.File))
			{
				//versione batch
				var commands = File.ReadAllLines(opts.File);
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
	}
}
