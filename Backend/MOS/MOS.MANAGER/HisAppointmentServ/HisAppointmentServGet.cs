using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentServ
{
    partial class HisAppointmentServGet : BusinessBase
    {
        internal HisAppointmentServGet()
            : base()
        {

        }

        internal HisAppointmentServGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_APPOINTMENT_SERV> Get(HisAppointmentServFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentServDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_APPOINTMENT_SERV GetById(long id)
        {
            try
            {
                return GetById(id, new HisAppointmentServFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_APPOINTMENT_SERV GetById(long id, HisAppointmentServFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentServDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_APPOINTMENT_SERV> GetByTreatmentId(long treatmentId)
        {
            try
            {
                HisAppointmentServFilterQuery filter = new HisAppointmentServFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                return this.Get(filter);
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
