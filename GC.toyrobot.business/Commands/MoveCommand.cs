using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
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
