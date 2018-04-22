using System;

namespace GC.ToyRobot.Commands
{
	abstract class BaseCommand<T> : ICommand where T : class
	{
		protected T _receiver;

		private BaseCommand() { }
		public BaseCommand(T receiver)
		{			
			this._receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
		}

		public abstract void Execute();
	}
}
