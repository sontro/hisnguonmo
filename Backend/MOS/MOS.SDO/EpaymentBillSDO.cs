using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class EpaymentBillSDO
    {
        public long RequestRoomId { get; set; }
        public long TreatmentId { get; set; }
        public string CardServiceCode { get; set; }
    }

    public class EpaymentBillResultSDO
    {
        public V_HIS_TRANSACTION Transaction { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
    }
}
