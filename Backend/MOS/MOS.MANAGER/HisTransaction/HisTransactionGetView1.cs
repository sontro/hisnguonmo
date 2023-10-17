using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionGet : GetBase
    {
        internal List<V_HIS_TRANSACTION_1> GetView1(HisTransactionView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANSACTION_1 GetView1ById(long id)
        {
            try
            {
                return GetView1ById(id, new HisTransactionView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANSACTION_1 GetView1ById(long id, HisTransactionView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionDAO.GetView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_TRANSACTION_1> GetView1ByIds(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisTransactionView1FilterQuery filter = new HisTransactionView1FilterQuery();
                filter.IDs = ids;
                return this.GetView1(filter);
            }
            return null;
        }

        internal V_HIS_TRANSACTION_1 GetView1ByCode(string code)
        {
            try
            {
                return GetView1ByCode(code, new HisTransactionView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANSACTION_1 GetView1ByCode(string code, HisTransactionView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionDAO.GetView1ByCode(code, filter.Query());
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
