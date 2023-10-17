using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.AntibioticRequest.ADO
{
	public class AntibioticMicrobiADO : HIS_ANTIBIOTIC_MICROBI
	{
		public int Action { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeSpecimens { get; set; }
		public string ErrorMessageSpecimens { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeImplanetionTime { get; set; }
		public string ErrorMessageImplanetionTime { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeResultTime { get; set; }
		public string ErrorMessageResultTime { get; set; }
		public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeResult { get; set; }
		public string ErrorMessageResult { get; set; }
	}
}
