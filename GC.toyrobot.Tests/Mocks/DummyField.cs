using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.Tests.Mocks
{
	class DummyField : business.IField
	{
		public bool IsValidPosition(Point destinationPoint)
		{
			throw new NotImplementedException();
		}
	}
}
