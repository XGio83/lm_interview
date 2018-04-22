namespace GC.ToyRobot.Commands
{
	class MoveCommand : BaseCommand<IRobot>
	{
		public MoveCommand(IRobot robot) : base(robot) { }
		public override void Execute()
		{
			_receiver.Move();
		}
	}
}
