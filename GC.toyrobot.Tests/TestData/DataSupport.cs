using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GC.ToyRobot.Tests.TestData
{
	class DataSupport
	{
		public static IEnumerable<string> GetDataCommands()
		{
			return File.ReadAllLines("TestData/Data1.txt", System.Text.Encoding.UTF8).Select(l => l.Trim());
		}

		public static IEnumerable<string> GetSpiralCommands()
		{
			return File.ReadAllLines("TestData/Spiral.txt", System.Text.Encoding.UTF8).Select(l => l.Trim());
		}
	}
}
