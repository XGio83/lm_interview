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

			//check all positions and extra boundaries
			for (int i = 0; i < table.TableSize.Width; i++)
			{
				for (int j = 0; j < table.TableSize.Height; j++)
				{
					Assert.IsTrue(table.IsValidPosition(new Point { X = i, Y = j }));
					Assert.IsFalse(table.IsValidPosition(new Point { X = -1, Y = j }));
					Assert.IsFalse(table.IsValidPosition(new Point { X = i, Y = -1 }));
					Assert.IsFalse(table.IsValidPosition(new Point { X = table.TableSize.Width, Y = j }));
					Assert.IsFalse(table.IsValidPosition(new Point { X = i, Y = table.TableSize.Height }));
				}
			}
		}
		
	}
}
