using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskContract
{
    partial class HisKskContractGet : BusinessBase
    {
        internal List<V_HIS_KSK_CONTRACT> GetView(HisKskContractViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskContractDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_CONTRACT GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisKskContractViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_KSK_CONTRACT> GetViewByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisKskContractViewFilterQuery filter = new HisKskContractViewFilterQuery();
                    filter.IDs = ids;
                    return this.GetView(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_CONTRACT GetViewById(long id, HisKskContractViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskContractDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_CONTRACT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisKskContractViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_CONTRACT GetViewByCode(string code, HisKskContractViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskContractDAO.GetViewByCode(code, filter.Query());
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
