using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
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
