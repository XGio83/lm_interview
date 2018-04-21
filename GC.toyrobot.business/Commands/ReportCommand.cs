using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class ReportCommand : BaseCommand<Robot>
	{
		Action<string> _callBack = null;
		public ReportCommand(Robot robot, Action<string> reportCallback) : base(robot) {
			_callBack = reportCallback;
		}
		public override void Execute()
		{
			var position = _receiver.ReportPosition();
			if (!string.IsNullOrWhiteSpace(position) && _callBack != null) _callBack.Invoke(position);
		}
	}
}
