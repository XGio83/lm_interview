using System.Drawing;

namespace GC.toyrobot.business
{
	interface IField
	{
		bool IsValidPosition(Point destinationPoint);
	}
}
