using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionUpdateInfoSDO
    {
        public long TransactionId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerAccountNumber { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerOrganization { get; set; }
        public long PayFormId { get; set; }
        public decimal? TransferAmount { get; set; }

        public long RequestRoomId { get; set; }
        public string Description { get; set; }
        public long? WorkingShiftId { get; set; }
        public long? AccountBookId { get; set; }
        public long? NumOrder { get; set; }

        public long? RepayReasonId { get; set; }
    }
}
