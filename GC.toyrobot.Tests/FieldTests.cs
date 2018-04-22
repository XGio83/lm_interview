using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace GC.toyrobot.Tests
{
	[TestClass]
	public class FieldTests
	{
		[TestMethod]
		[Description("Test di controllo field")]
		public void Field_CheckValidPosition()
		{
			var table = new business.Tabletop(new Size(5, 5));
			Assert.IsTrue(table.IsValidPosition(new Point { X = 0, Y = 0 }));
			Assert.IsTrue(table.IsValidPosition(new Point { X = 1, Y = 1 }));
			Assert.IsFalse(table.IsValidPosition(new Point { X = 6, Y = 5 }));
			Assert.IsFalse(table.IsValidPosition(new Point { X = 5, Y = 6 }));
		}
		
	}
}
