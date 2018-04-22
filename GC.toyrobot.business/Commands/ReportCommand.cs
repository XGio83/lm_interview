using System;

namespace GC.ToyRobot.Commands
{
	class ReportCommand : BaseCommand<IRobot>
	{
		Action<string> _callBack = null;
		public ReportCommand(IRobot robot, Action<string> reportCallback) : base(robot) {
			_callBack = reportCallback;
		}
		public override void Execute()
		{
			var position = _receiver.ReportPosition();
			if (!string.IsNullOrWhiteSpace(position) && _callBack != null) _callBack.Invoke(position);
		}
	}
}
