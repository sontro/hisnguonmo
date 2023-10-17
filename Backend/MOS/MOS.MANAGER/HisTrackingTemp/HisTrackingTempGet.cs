using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTrackingTemp
{
    partial class HisTrackingTempGet : BusinessBase
    {
        internal HisTrackingTempGet()
            : base()
        {

        }

        internal HisTrackingTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRACKING_TEMP> Get(HisTrackingTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTrackingTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRACKING_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisTrackingTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRACKING_TEMP GetById(long id, HisTrackingTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTrackingTempDAO.GetById(id, filter.Query());
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
