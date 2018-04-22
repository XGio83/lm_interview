namespace GC.ToyRobot.Commands
{
	class LeftCommand : BaseCommand<IRobot>
	{
		public LeftCommand(IRobot robot) : base(robot) { }

		public override void Execute()
		{
			this._receiver.CounterClockwiseTurn();
		}
	}
}
