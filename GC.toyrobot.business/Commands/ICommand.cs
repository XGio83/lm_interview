using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business.Commands
{
	interface ICommand
	{
		void Execute();
		string Result { get; }
	}
}
