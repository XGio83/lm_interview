using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GC.toyrobot.business;

namespace GC.toyrobot.Tests
{
	[TestClass]
	public class RobotTests
	{
		private Robot _robot;
		private Tabletop _table;

		[TestInitialize]
		public void Init()
		{
			_table = new Tabletop(new System.Drawing.Size(5, 5));
			_robot = new Robot(_table);
		}

		[TestCleanup]
		public void Clean()
		{
			_table = null;
			_robot = null;
		}

		[TestMethod]
		[Description("")]
		public void Robot_SimpleMove()
		{
			/*
				PLACE 0,0,NORTH
				MOVE
				REPORT
			*/
			_robot.Place(0, 0, Directions.NORTH);
			_robot.Move();
			Assert.AreEqual(_robot.ReportPosition(), "0,1,NORTH");
		}

		[TestMethod]
		[Description("")]
		public void Robot_SimpleMove2()
		{
			/*
				PLACE 0,0,NORTH
				LEFT
				REPORT
			*/
			_robot.Place(0, 0, Directions.NORTH);
			_robot.CounterClockwiseTurn();
			Assert.AreEqual(_robot.ReportPosition(), "0,0,WEST");
		}

		[TestMethod]
		[Description("")]
		public void Robot_SimpleMove3()
		{
			/*
				PLACE 1,2,EAST
				MOVE
				MOVE
				LEFT
				MOVE
				REPORT
			*/
			_robot.Place(1, 2, Directions.EAST);
			_robot.Move();
			_robot.Move();
			_robot.CounterClockwiseTurn();
			_robot.Move();
			Assert.AreEqual(_robot.ReportPosition(), "3,3,NORTH");
		}

		[TestMethod]
		[Description("Check robot clockwise turn")]
		public void Robot_ClockwiseDirection()
		{
			var position = string.Empty;
			_robot.Place(0, 0, Directions.NORTH);
			_robot.ClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.EAST.ToString()));
			_robot.ClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.SOUTH.ToString()));
			_robot.ClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.WEST.ToString()));
			_robot.ClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.NORTH.ToString()));
			_robot.ClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.EAST.ToString()));
		}

		[TestMethod]
		[Description("Check robot counterclockwise turn")]
		public void Robot_CounterclockwiseDirection()
		{
			var position = string.Empty;
			_robot.Place(0, 0, Directions.NORTH);
			_robot.CounterClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.WEST.ToString()));
			_robot.CounterClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.SOUTH.ToString()));
			_robot.CounterClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.EAST.ToString()));
			_robot.CounterClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.NORTH.ToString()));
			_robot.CounterClockwiseTurn();
			position = _robot.ReportPosition();
			Assert.IsTrue(position.EndsWith(Directions.WEST.ToString()));
		}
	}
}
