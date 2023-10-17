using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
	public enum DocumentStatusEnum
	{
		UNSIGN = 1,
		SIGNING = 2,
		SIGNING_FINISHED = 3,
		SIGNING_REJECTED = 4,
        UNSAVE_OR_DELETED = 5
	}
	
	public class EmrDocumentChangeStatusSDO
	{
		public string HisCode { get; set; }
		public string DocumentCode { get; set; }
		public string LastVersionUrl { get; set; }
		public DocumentStatusEnum DocumentStatus { get; set; }
	}
}
