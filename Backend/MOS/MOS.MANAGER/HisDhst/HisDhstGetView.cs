using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDhst
{
    partial class HisDhstGet : GetBase
    {
        internal List<V_HIS_DHST> GetView(HisDhstViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDhstDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DHST GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisDhstViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DHST GetViewById(long id, HisDhstViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDhstDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_DHST> GetViewByTreatmentId(long id)
        {
            try
            {
                HisDhstViewFilterQuery filter = new HisDhstViewFilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_DHST> GetViewByTrackingId(long trackingId)
        {
            try
            {
                HisDhstViewFilterQuery filter = new HisDhstViewFilterQuery();
                filter.TRACKING_ID = trackingId;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_DHST> GetViewByCareId(long careId)
        {
            HisDhstViewFilterQuery filter = new HisDhstViewFilterQuery();
            filter.CARE_ID = careId;
            return this.GetView(filter);
        }

        internal List<V_HIS_DHST> GetViewByTreatmentIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisDhstViewFilterQuery filter = new HisDhstViewFilterQuery();
                    filter.TREATMENT_IDs = ids;
                    return this.GetView(filter);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

    }
}
