using System.Drawing;

namespace GC.ToyRobot
{
	public class Tabletop: IField
	{
		private Size _tableSize;

		public Size TableSize => _tableSize;

		public Tabletop(Size tableSize)
		{
			this._tableSize = tableSize;
		}
	
		//per il falling check
		public bool IsValidPosition(Point destinationPoint) {
			return destinationPoint.X < this._tableSize.Width && destinationPoint.X >= 0 && destinationPoint.Y < this._tableSize.Height && destinationPoint.Y >= 0;
		}
	}
}
