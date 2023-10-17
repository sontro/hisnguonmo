using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MRSummaryDetail.ADO
{
	public class SummaryDetail
	{
		public SummaryDetail() { }
		public string CHECK_ITEM_TYPE_NAME { get; set; }
		public bool IS_SELF_CHECK { get; set; }
		public bool IS_CHECKER_CHECK { get; set; }
		public bool IS_CHECKER_NOT_USED { get; set; }
		public string NOTE { get; set; }
		public string PARENT { get; set; }

		public long SUMMARY_ID { get; set; }
		public long CHECK_ITEM_ID { get; set; }
		public long CHECK_LIST_ITEM_ID { get; set; }

		public long? NUM_ORDER_PARENT { get; set; }
		public long? NUM_ORDER_CHILD { get; set; }
	}
}
