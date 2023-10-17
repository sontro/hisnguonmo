using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class EpaymentDepositResultSDO
    {
        public V_HIS_TRANSACTION Transaction {get;set;}
        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposit { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
    }
}
