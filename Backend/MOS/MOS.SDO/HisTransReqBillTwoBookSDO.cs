using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransReqBillTwoBookSDO
    {
        public HIS_TRANS_REQ RecieptTransReq { get; set; }
        public HIS_TRANS_REQ InvoiceTransReq { get; set; }

        public List<HIS_SESE_TRANS_REQ> RecieptSeseTransReqs { get; set; }
        public List<HIS_SESE_TRANS_REQ> InvoiceSeseTransReqs { get; set; }

        public decimal RecieptPayAmount { get; set; }
        public decimal InvoicePayAmount { get; set; }
        public long RequestRoomId { get; set; }
        public long TreatmentId { get; set; }
    }
}
