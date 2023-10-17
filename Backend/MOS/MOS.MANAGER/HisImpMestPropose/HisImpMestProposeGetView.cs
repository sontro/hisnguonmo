using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeGet : BusinessBase
    {
        internal List<V_HIS_IMP_MEST_PROPOSE> GetView(HisImpMestProposeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_PROPOSE GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisImpMestProposeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_PROPOSE GetViewById(long id, HisImpMestProposeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_IMP_MEST_PROPOSE> GetViewByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisImpMestProposeViewFilterQuery filter = new HisImpMestProposeViewFilterQuery();
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
    }
}
