using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    partial class HisVaccAppointmentGet : BusinessBase
    {
        internal HisVaccAppointmentGet()
            : base()
        {

        }

        internal HisVaccAppointmentGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACC_APPOINTMENT> Get(HisVaccAppointmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccAppointmentDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_APPOINTMENT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccAppointmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_APPOINTMENT GetById(long id, HisVaccAppointmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccAppointmentDAO.GetById(id, filter.Query());
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
