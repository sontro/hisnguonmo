using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.ADO
{
	public class AntibioticRequestADO
	{
		public V_HIS_ANTIBIOTIC_REQUEST AntibioticRequest { get; set; }
		public string PatientCode { get; set; }
		public string PatientName { get; set; }
		public long Dob { get; set; }
		public bool IsHasNotDayDob { get; set; }
		public string GenderName { get; set; }
		public decimal? Temperature { get; set; }
		public decimal? Weight { get; set; }
		public decimal? Height { get; set; }
		public string IcdSubCode { get; set; }
		public string IcdText { get; set; }
		public long ExpMestId { get; set; }
		public List<HIS_ANTIBIOTIC_NEW_REG> NewRegimen { get; set; }

		public ProcessType? processType { get; set; }
		public enum ProcessType
		{
			Request,
			Approval
		}

	}
}
