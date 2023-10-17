using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    partial class HisHoreHohaGet : BusinessBase
    {
        internal List<V_HIS_HORE_HOHA> GetView(HisHoreHohaViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHohaDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_HORE_HOHA GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisHoreHohaViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_HORE_HOHA GetViewById(long id, HisHoreHohaViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHohaDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_HORE_HOHA> GetViewByHoreHandoverId(long horeHandoverId)
        {
            try
            {
                HisHoreHohaViewFilterQuery filter = new HisHoreHohaViewFilterQuery();
                filter.HORE_HANDOVER_ID = horeHandoverId;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
