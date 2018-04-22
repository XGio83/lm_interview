using System;
using System.Drawing;

namespace GC.ToyRobot.Tests.Mocks
{
	class DummyRobot : IRobot
	{
		public void ClockwiseTurn()
		{
			throw new NotImplementedException();
		}

		public void CounterClockwiseTurn()
		{
			throw new NotImplementedException();
		}

		public bool Move()
		{
			throw new NotImplementedException();
		}

		public bool Place(byte x, byte y, Directions direcion)
		{
			throw new NotImplementedException();
		}

		public bool Place(Point position, Directions direction)
		{
			throw new NotImplementedException();
		}

		public string ReportPosition()
		{
			throw new NotImplementedException();
		}
	}
}
