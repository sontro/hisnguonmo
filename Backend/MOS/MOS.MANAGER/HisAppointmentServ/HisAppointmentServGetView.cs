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
        internal List<V_HIS_APPOINTMENT_SERV> GetView(HisAppointmentServViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentServDAO.GetView(filter.Query(), param);
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
