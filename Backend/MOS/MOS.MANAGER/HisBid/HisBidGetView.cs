using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.MANAGER.HisBid
{
    partial class HisBidGet : BusinessBase
    {
        internal List<V_HIS_BID> GetView(HisBidViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_BID> GetViewBySupplier(long supplierId)
        {
            List<V_HIS_BID> result = null;
            try
            {
                string sql = new StringBuilder().Append("SELECT BID.* FROM V_HIS_BID BID WHERE ")
                    .Append("EXISTS (SELECT 1 FROM HIS_BID_MEDICINE_TYPE WHERE BID_ID = BID.ID AND SUPPLIER_ID = :param1) ")
                    .Append("OR EXISTS (SELECT 1 FROM HIS_BID_MATERIAL_TYPE WHERE BID_ID = BID.ID AND SUPPLIER_ID = :param2)").ToString();
                result = DAOWorker.SqlDAO.GetSql<V_HIS_BID>(sql, supplierId, supplierId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }

        internal List<V_HIS_BID_1> GetView1(HisBidView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
