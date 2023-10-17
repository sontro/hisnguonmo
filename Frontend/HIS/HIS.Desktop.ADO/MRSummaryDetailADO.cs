using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
	public class MRSummaryDetailADO
	{
		public long TreatmentId { get; set; }
		public HIS_MR_CHECK_SUMMARY CheckSummary { get; set; }
		public OpenFrom? processType { get; set; }
		public enum OpenFrom
		{
			TreatmentLatchApproveStore,
			MedicalStoreV2,
			TreatmentList
		}
	}
}
