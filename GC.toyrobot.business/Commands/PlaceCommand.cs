using System.Drawing;

namespace GC.ToyRobot.Commands
{
	class PlaceCommand : BaseCommand<IRobot>
	{
		Point _placePoint;
		Directions _direction;

		public PlaceCommand(IRobot robot, byte x, byte y, Directions direction) : this(robot, new Point(x, y), direction) {}

		public PlaceCommand(IRobot robot, Point placePoint, Directions direction) : base(robot)
		{
			_placePoint = placePoint;
			_direction = direction;
		}
		public override void Execute()
		{
			_receiver.Place(_placePoint, _direction);
		}
	}
}
