using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GC.toyrobot.business;
using GC.toyrobot.business.Commands;
using System.Diagnostics;
using Autofac;
using System.Drawing;

namespace GC.toyrobot.Tests
{
	[TestClass]
	public class RobotTests
	{
		private static IContainer _diContainer;
		private IRobot _robot;
		private const int ROBOTSPEED = 1;
		private const int SQUAREDTABLESIZE = 5;
		

		[TestInitialize]
		public void Init()
		{
			registerTypes();
			_robot = _diContainer.Resolve<IRobot>();
		}

		[TestCleanup]
		public void cleanup()
		{
			//pulisci il container
			_diContainer.Dispose();
			_diContainer = null;
			_robot = null;			
		}

		[TestMethod]
		public void Robot_ClockwiseDirection()
		{
			_robot.Place(0, 0, Directions.NORTH);
			_robot.ClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.EAST.ToString()));
			_robot.ClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.SOUTH.ToString()));
			_robot.ClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.WEST.ToString()));
			_robot.ClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.NORTH.ToString()));
			_robot.ClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.EAST.ToString()));
		}

		[TestMethod]
		public void Robot_CounterclockwiseDirection()
		{
			_robot.Place(0, 0, Directions.NORTH);
			_robot.CounterClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.WEST.ToString()));
			_robot.CounterClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.SOUTH.ToString()));
			_robot.CounterClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.EAST.ToString()));
			_robot.CounterClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.NORTH.ToString()));
			_robot.CounterClockwiseTurn();
			Assert.IsTrue(_robot.ReportPosition().EndsWith(Directions.WEST.ToString()));
		}

		[TestMethod]
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
		public void Robot_SimpleMove_WithTurn()
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
		public void Robot_SimpleMove_WithIgnore()
		{
			/*
				PLACE 7,2,EAST
				PLACE 1,2,EAST
				MOVE
				MOVE
				LEFT
				MOVE
				REPORT
			*/
			_robot.Place(7, 2, Directions.EAST);
			_robot.Place(1, 2, Directions.EAST);
			_robot.Move();
			_robot.Move();
			_robot.CounterClockwiseTurn();
			_robot.Move();
			Assert.AreEqual(_robot.ReportPosition(), "3,3,NORTH");
		}

		[TestMethod]
		public void Movement_FromFile_NoCommands()
		{
			var reportedPosition = string.Empty;
			foreach (var command in TestData.DataSupport.GetDataCommands())
			{
				var placeMatch = Regex.Match(command, @"^PLACE (\d+),(\d+),(NORTH|EAST|SOUTH|WEST)$");
				if (placeMatch.Success)
					_robot.Place(byte.Parse(placeMatch.Groups[1].Value), byte.Parse(placeMatch.Groups[2].Value), (Directions)Enum.Parse(typeof(Directions), placeMatch.Groups[3].Value));
				else
				{
					switch (command)
					{
						case "MOVE":
							_robot.Move();
							break;
						case "LEFT":
							_robot.CounterClockwiseTurn();
							break;
						case "RIGHT":
							_robot.ClockwiseTurn();
							break;
						case "REPORT":
							reportedPosition = _robot.ReportPosition();
							break;
					}
				}			
			}

			Assert.AreEqual("3,3,NORTH", reportedPosition);
		}
		
		[TestMethod]
		public void Commands_puppeteer_enqueue()
		{
			Action<string> reportCallback = (string rep) => {
				Debug.WriteLine(rep);
				Assert.AreEqual("3,3,NORTH", rep);
			};
			var puppeteer = new Puppeteer(_robot, reportCallback);
			puppeteer.EnqueueCommands(TestData.DataSupport.GetDataCommands().ToList());			
			puppeteer.ExecuteQueue();
		}

		[TestMethod]
		public void Commands_puppeteer_enqueue_spiral()
		{
			Action<string> reportCallback = (string rep) => {
				Debug.WriteLine(rep);
				Assert.AreEqual("2,2,EAST", rep);
			};
			var puppeteer = new Puppeteer(_robot, reportCallback);
			puppeteer.EnqueueCommands(TestData.DataSupport.GetSpiralCommands().ToList());
			puppeteer.ExecuteQueue();
		}

		private static void registerTypes()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<GC.toyrobot.business.Robot>().As<GC.toyrobot.business.IRobot>().WithParameter("robotSpeed", ROBOTSPEED);
			builder.RegisterType<GC.toyrobot.business.Tabletop>().As<GC.toyrobot.business.IField>().WithParameter("tableSize", new Size(SQUAREDTABLESIZE, SQUAREDTABLESIZE));
			_diContainer = builder.Build();
		}

	}
}
