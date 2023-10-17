using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.AntibioticRequest.ADO
{
	public class AntibioticNewRegADO : V_HIS_ANTIBIOTIC_NEW_REG
	{
		public int Action { get; set; }

		public bool ANTIBIOTIC_NEW_ADD { get; set; }
		public bool ANTIBIOTIC_DELETE { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeConcentra { get; set; }
		public string ErrorMessageConcentra { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeDosage { get; set; }
		public string ErrorMessageDosage { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypePeriod { get; set; }
		public string ErrorMessagePeriod { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeUseForm { get; set; }
		public string ErrorMessageUseForm { get; set; }
	}
}
