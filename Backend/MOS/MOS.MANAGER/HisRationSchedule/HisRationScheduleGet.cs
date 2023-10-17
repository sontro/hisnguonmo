using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleGet : BusinessBase
    {
        internal HisRationScheduleGet()
            : base()
        {

        }

        internal HisRationScheduleGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_RATION_SCHEDULE> Get(HisRationScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationScheduleDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SCHEDULE GetById(long id)
        {
            try
            {
                return GetById(id, new HisRationScheduleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SCHEDULE GetById(long id, HisRationScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationScheduleDAO.GetById(id, filter.Query());
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
