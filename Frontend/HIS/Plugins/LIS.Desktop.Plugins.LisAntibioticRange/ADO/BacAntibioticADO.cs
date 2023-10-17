using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisAntibioticRange.ADO
{
	public class BacAntibioticADO : LIS_BAC_ANTIBIOTIC
	{
		public string ANTIBIOTIC_NAME { get; set; }

		public string ANTIBIOTIC_CODE { get; set; }

		public string BACTERIUM_NAME { get; set; }

		public string BACTERIUM_CODE { get; set; }
	}
}
