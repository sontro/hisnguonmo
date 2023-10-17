using HIS.Desktop.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute.ADO
{
	public class InformationADO
	{
		public long? EKIP_TEMP_ID { get; set; }
		public long? EKIP_DEPARTMENT_ID { get; set; }
		public List<HisEkipUserADO> ListEkipUser { get; set; }
	}
}
