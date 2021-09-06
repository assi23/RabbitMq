using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Models
{
	public class Animes:Base
	{
		public string Name { get; set; }
		public int Episode { get; set; }
		public int Season { get; set; }
	}
}
