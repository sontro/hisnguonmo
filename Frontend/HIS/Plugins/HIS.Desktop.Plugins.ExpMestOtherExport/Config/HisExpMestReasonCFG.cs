using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestOtherExport.Conf
{
    public class HisExpMestReasonCFG
    {
        private static long hisExpMestReasonIdThanhLy;
        public static long HisExpMestReasonId__ThanhLy
        {
            get
            {
                if (hisExpMestReasonIdThanhLy <= 0)
                {
                    hisExpMestReasonIdThanhLy = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("DBCODE.HIS_RS.HIS_EXP_MEST_REASON.THANH_LY"));
                }
                return hisExpMestReasonIdThanhLy;
            }
        }


        private static long GetId(string expMestReasonCode)
        {
            long result = 0;
            try
            {
                if (String.IsNullOrEmpty(expMestReasonCode)) throw new NullReferenceException("Code");
                var transactionType = BackendDataWorker.Get<HIS_EXP_MEST_REASON>().FirstOrDefault(o => o.EXP_MEST_REASON_CODE == expMestReasonCode);
                if (transactionType != null)
                {
                    result = transactionType.ID;
                }
                if (result == 0) throw new NullReferenceException(expMestReasonCode);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
