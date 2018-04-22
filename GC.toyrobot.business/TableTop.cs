using System.Drawing;

namespace GC.toyrobot.business
{
	public class Tabletop: IField
	{
		private Size _tableSize;

		public Tabletop(Size tableSize)
		{
			this._tableSize = tableSize;
		}

		//per il falling check
		public bool IsValidPosition(Point destinationPoint) {
			return destinationPoint.X <= this._tableSize.Width && destinationPoint.Y <= this._tableSize.Height;
		}
	}
}
