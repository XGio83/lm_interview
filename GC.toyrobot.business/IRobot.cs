using System.Drawing;

namespace GC.ToyRobot
{
	public interface IRobot
	{
		bool Place(byte x, byte y, Directions direcion);
		bool Place(Point position, Directions direction);
		bool Move();
		void ClockwiseTurn();
		void CounterClockwiseTurn();
		string ReportPosition();
	}
}
