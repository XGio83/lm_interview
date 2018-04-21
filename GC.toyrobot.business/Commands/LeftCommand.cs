using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class LeftCommand : BaseCommand<Robot>
	{
		public LeftCommand(Robot robot) : base(robot) { }

		public override void Execute()
		{
			this._receiver.CounterClockwiseTurn();
		}
	}
}
