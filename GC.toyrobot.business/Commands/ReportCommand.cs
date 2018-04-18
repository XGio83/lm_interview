using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class ReportCommand : BaseCommand<Robot>
	{
		string _result = string.Empty;
		public ReportCommand(Robot robot) : base(robot) { }
		public override string Result => _result;
		public override void Execute()
		{
			this._result = _receiver.ReportPosition();
		}
	}
}
