using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute.ADO
{
    class ThreadSereServADO
    {
        public List<HIS_SERE_SERV> ListHisSereServ { get; set; }//out
        public Dictionary<long, List<HIS_SERE_SERV_BILL>> DictHisSersServBill { get; set; }//out, key: SERE_SERV_ID
        public Dictionary<long, List<HIS_SERE_SERV_DEPOSIT>> DictHisSereServDeposit { get; set; }//out, key: SERE_SERV_ID
        public Dictionary<long, List<HIS_SESE_DEPO_REPAY>> ListHisSeseDepoRepay { get; set; }//out, Key:SERE_SERV_DEPOSIT_ID
    }
}
