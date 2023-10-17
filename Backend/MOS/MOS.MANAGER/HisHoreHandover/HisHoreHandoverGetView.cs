using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandover
{
    partial class HisHoreHandoverGet : BusinessBase
    {
        internal List<V_HIS_HORE_HANDOVER> GetView(HisHoreHandoverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal V_HIS_HORE_HANDOVER GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisHoreHandoverViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_HORE_HANDOVER GetViewById(long id, HisHoreHandoverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverDAO.GetViewById(id, filter.Query());
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
