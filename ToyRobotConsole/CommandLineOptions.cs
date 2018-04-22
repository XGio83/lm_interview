using CommandLine;

namespace GC.ToyRobot.Console
{
	class CommandLineOptions
	{
		[Option('f',"file",HelpText ="File with text commands")]
		public string File { get; set; }
		[Option('t', "table", HelpText = "table size", Required =true)]
		public int TableSize { get; set; }
		[Option('s', "speed", HelpText = "robot speed", Required = true)]
		public byte RobotSpeed { get; set; }
	}
}
