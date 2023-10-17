using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class RepayServiceADO
    {
        public V_HIS_TREATMENT_FEE hisTreatment { get; set; }
        public long? branchId { get; set; }
        public HisTransactionRepaySDO repaySdo { get; set; }
        public long cashierRoomId { get; set; }
        public List<V_HIS_SERE_SERV_5> ListSereServ { get; set; }

        public RepayServiceADO()
        { }

        public RepayServiceADO(V_HIS_TREATMENT_FEE _hisTreatment, HisTransactionRepaySDO _transactionData, long _branchId, long _cashierRoomId, List<V_HIS_SERE_SERV_5> _listSereServ)
        {
            try
            {
                this.hisTreatment = _hisTreatment;
                this.repaySdo = _transactionData;
                this.branchId = _branchId;
                this.cashierRoomId = _cashierRoomId;
                this.ListSereServ = _listSereServ;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
