using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.ADO
{
	public class BorrowADO
	{
		public string LOGINNAME { get; set; }
		public string CARER_CARD_NUMBER {get;set;}
		public long CARER_CARD_ID { get; set; }
		public long TIME_FROM { get; set; }
		public string SERVICE_NAME { get; set; }
		public long ID_ROW { get; set; }
		public V_HIS_CARER_CARD HIS_CARER_CARD { get; set; }
	}
}
