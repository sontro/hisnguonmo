using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
	public class PresMaterialADO : MOS.SDO.PresMaterialSDO
	{
		public long useTime { get; set; }
		public PresMaterialADO() { }
		public PresMaterialADO (MOS.SDO.PresMaterialSDO sdo)
		{
			try
			{
				if (sdo != null)
				{
					Inventec.Common.Mapper.DataObjectMapper.Map<PresMaterialADO>(this, sdo);
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
	}
}
