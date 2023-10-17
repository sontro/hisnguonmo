using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebateDiagnostic.ADO
{
	public class InvateADO
	{
		public InvateADO(long? v1, string v2)
		{
			ID = v1;
			NAME = v2;
		}

		public long? ID { get; set; }
		public string NAME { get; set; }
	}
}
