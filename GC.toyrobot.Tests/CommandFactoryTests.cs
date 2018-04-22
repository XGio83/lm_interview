using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using GC.toyrobot.business;
using GC.toyrobot.business.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GC.toyrobot.Tests
{
	[TestClass]
	public class CommandFactoryTests
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
		public void Commands_factory_commandParsing()
		{
			var commandList = new List<BaseCommand<IRobot>>();
			foreach (var command in TestData.DataSupport.GetStringCommands())
			{
				commandList.Add(RobotCommandFactory.Creator.GetCommand(_robot, command, null));
			}
			Assert.IsTrue(commandList.Count == 9);
			Assert.IsInstanceOfType(commandList[0], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[1], typeof(LeftCommand));
			Assert.IsInstanceOfType(commandList[2], typeof(PlaceCommand));
			Assert.IsInstanceOfType(commandList[3], typeof(PlaceCommand));
			Assert.IsInstanceOfType(commandList[4], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[5], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[6], typeof(LeftCommand));
			Assert.IsInstanceOfType(commandList[7], typeof(MoveCommand));
			Assert.IsInstanceOfType(commandList[8], typeof(ReportCommand));
		}

		private static void registerTypes()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<Mocks.DummyRobot>().As<IRobot>();
			builder.RegisterType<Mocks.DummyField>().As<IField>();
			_diContainer = builder.Build();
		}
	}
}
