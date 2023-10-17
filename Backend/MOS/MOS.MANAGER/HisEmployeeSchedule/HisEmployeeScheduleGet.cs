using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleGet : BusinessBase
    {
        internal HisEmployeeScheduleGet()
            : base()
        {

        }

        internal HisEmployeeScheduleGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EMPLOYEE_SCHEDULE> Get(HisEmployeeScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeScheduleDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMPLOYEE_SCHEDULE GetById(long id)
        {
            try
            {
                return GetById(id, new HisEmployeeScheduleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMPLOYEE_SCHEDULE GetById(long id, HisEmployeeScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeScheduleDAO.GetById(id, filter.Query());
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
