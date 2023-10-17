using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.ADO
{
	public class PresMedicineADO : MOS.SDO.PresMedicineSDO
	{
		public long useTime { get; set; }
		public PresMedicineADO() { }
		public PresMedicineADO(MOS.SDO.PresMedicineSDO sdo)
		{
			try
			{
				if (sdo != null)
				{
					Inventec.Common.Mapper.DataObjectMapper.Map<PresMedicineADO>(this, sdo);
				}
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
	}
}
