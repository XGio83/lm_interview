using System.Drawing;

namespace GC.toyrobot.business
{
	public interface IField
	{
		bool IsValidPosition(Point destinationPoint);
	}
}
