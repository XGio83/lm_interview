﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace GC.ToyRobot
{
	public class Robot: IRobot
	{
		//offset moves
		private Dictionary<Directions, Point> _movingOffsets;

		private Point _currentPosition;
		private Directions _currentDirection;
		private Status _currentStatus;
		private IField _table;

		public Directions CurrentDirection => _currentDirection;
		public Status Status => _currentStatus;

		public Robot(IField table, byte robotSpeed = 1)
		{
			this._table = table ?? throw new ArgumentNullException(nameof(table));
			this._currentPosition = new Point(0, 0);
			this._currentDirection = Directions.NORTH;
			this._currentStatus = Status.AwayFromTable;			
			this.initOffsets(robotSpeed);
		}

		private void initOffsets(byte speed)
		{
			this._movingOffsets = new Dictionary<Directions, Point>() {
				[Directions.NORTH] = new Point(0, speed),
				[Directions.EAST] = new Point(speed, 0),
				[Directions.SOUTH] = new Point(0, -speed),
				[Directions.WEST] = new Point(-speed, 0),
			};
		}

		public bool Move()
		{
			if (isPlaced())
			{
				var nextPosition = new Point(_currentPosition.X, _currentPosition.Y);
				nextPosition.Offset(_movingOffsets[this._currentDirection]);
				if (_table.IsValidPosition(nextPosition))
				{
					_currentPosition = nextPosition;
					return true;
				}
			}			
			return false;			
		}
		public bool Place(byte x, byte y, Directions direcion)
		{
			return this.Place(new Point(x, y), direcion);
		}

		public bool Place(Point position, Directions direction)
		{
			if (_table.IsValidPosition(position))
			{
				this._currentStatus = Status.Placed;
				this._currentPosition = position;
				this._currentDirection = direction;
				return true;
			}
			return false;
		}
	
		public void ClockwiseTurn()
		{
			if (isPlaced())
				this._currentDirection = this._currentDirection == Directions.WEST ? Directions.NORTH : ++this._currentDirection;
		}

		public void CounterClockwiseTurn()
		{
			if (isPlaced())
				this._currentDirection = this._currentDirection == Directions.NORTH ? Directions.WEST : --this._currentDirection;
		}

		public string ReportPosition()
		{
			if (isPlaced())
				return $"{this._currentPosition.X},{this._currentPosition.Y},{this._currentDirection}";
			return string.Empty;
		}

		private bool isPlaced()
		{
			return _currentStatus == Status.Placed;
		}		
	}

	public enum Status
	{
		Placed,
		AwayFromTable
	}
}
