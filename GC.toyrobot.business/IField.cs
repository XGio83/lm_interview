using System.Drawing;

namespace GC.ToyRobot
{
	public interface IField
	{
		bool IsValidPosition(Point destinationPoint);
	}
}
