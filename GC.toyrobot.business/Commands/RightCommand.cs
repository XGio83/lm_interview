namespace GC.ToyRobot.Commands
{
	class RightCommand : BaseCommand<IRobot>
	{
		public RightCommand(IRobot robot) : base(robot) { }
		public override void Execute()
		{
			_receiver.ClockwiseTurn();
		}
	}
}
