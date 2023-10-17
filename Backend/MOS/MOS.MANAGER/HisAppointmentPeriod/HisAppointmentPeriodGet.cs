using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodGet : BusinessBase
    {
        internal HisAppointmentPeriodGet()
            : base()
        {

        }

        internal HisAppointmentPeriodGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_APPOINTMENT_PERIOD> Get(HisAppointmentPeriodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentPeriodDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_APPOINTMENT_PERIOD GetById(long id)
        {
            try
            {
                return GetById(id, new HisAppointmentPeriodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_APPOINTMENT_PERIOD GetById(long id, HisAppointmentPeriodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentPeriodDAO.GetById(id, filter.Query());
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
