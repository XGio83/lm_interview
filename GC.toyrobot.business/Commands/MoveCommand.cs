using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	class MoveCommand : BaseCommand<Robot>
	{
		public MoveCommand(Robot robot) : base(robot) { }

		public override string Result => string.Empty;
		public override void Execute()
		{
			_receiver.Move();
		}
	}
}
