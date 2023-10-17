using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PrepareAndExport.ADO
{
	internal class ExpMestADO : HIS_EXP_MEST
	{
		public string FILTER_TREATMENT_CODE { get; set; }
		public ExpMestADO() { }
		public ExpMestADO(HIS_EXP_MEST expMest)
		{
			try
			{
				Inventec.Common.Mapper.DataObjectMapper.Map<HIS_EXP_MEST>(this, expMest);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
			}
		}
	}
}
