using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServBill
{
    partial class HisSereServBillGet : BusinessBase
    {
        internal List<V_HIS_SERE_SERV_BILL> GetView(HisSereServBillViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServBillDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_BILL> GetViewBySereServIds(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSereServBillViewFilterQuery filter = new HisSereServBillViewFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                return this.GetView(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV_BILL> GetViewNoCancelBySereServIds(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSereServBillViewFilterQuery filter = new HisSereServBillViewFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                filter.IS_NOT_CANCEL = true;
                return this.GetView(filter);
            }
            return null;
        }

        internal List<V_HIS_SERE_SERV_BILL> GetViewByBillId(long billId)
        {
            HisSereServBillViewFilterQuery filter = new HisSereServBillViewFilterQuery();
            filter.BILL_ID = billId;
            return this.GetView(filter);
        }
    }
}
