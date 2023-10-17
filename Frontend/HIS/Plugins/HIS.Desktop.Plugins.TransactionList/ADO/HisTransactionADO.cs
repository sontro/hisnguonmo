using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.ADO
{
    public class HisTransactionADO : V_HIS_TRANSACTION
    {
        public long OLD_NUM_ORDER { get; set; }
        public string Status { get; set; }

        public void vHisTransaction() { }

        public void vHisTransaction(MOS.EFMODEL.DataModels.V_HIS_TRANSACTION data)
        {
            try
            {
                if (data != null)
                {

                    this.Status = data.IS_CANCEL == 1 ? "Đã hủy" : "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
