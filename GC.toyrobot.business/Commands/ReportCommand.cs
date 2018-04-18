using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class ReportCommand : BaseCommand<Robot>
	{
		public ReportCommand(Robot robot) : base(robot) { }
		public override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
