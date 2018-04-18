using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class PlaceCommand : BaseCommand<Robot>
	{
		Point _placePoint;
		Directions _direction;

		public PlaceCommand(Robot robot, byte x, byte y, Directions direction) : this(robot, new Point(x, y), direction) {}

		public PlaceCommand(Robot robot, Point placePoint, Directions direction) : base(robot)
		{
			_placePoint = placePoint;
			_direction = direction;
		}
		public override string Result => string.Empty;
		public override void Execute()
		{
			_receiver.Place(_placePoint, _direction);
		}
	}
}
