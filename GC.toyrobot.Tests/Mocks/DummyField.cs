using System;
using System.Drawing;

namespace GC.ToyRobot.Tests.Mocks
{
	class DummyField : IField
	{
		public bool IsValidPosition(Point destinationPoint)
		{
			throw new NotImplementedException();
		}
	}
}
