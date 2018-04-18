using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.toyrobot.business
{
	class Tabletop
	{
		private Size _tableSize;

		public Tabletop(Size tableSize)
		{
			this._tableSize = tableSize;
		}

		//per il falling check
		public bool IsValidPosition(Point destinationPoint) { //solo nel comando di move e di place chiamare questa funzione che in ingresso avrà la posizione destinazione. se il comando di place non va a buon fine il robot non si può mouvere. mettere uno stato al robot che ne identifica lo stato di "placed" oppure no
			return destinationPoint.X <= this._tableSize.Width && destinationPoint.Y <= this._tableSize.Height;
		}
	}
}
