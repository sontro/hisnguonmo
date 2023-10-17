using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentGet : BusinessBase
    {
        internal V_HIS_HIV_TREATMENT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisHivTreatmentViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_HIV_TREATMENT GetViewByCode(string code, HisHivTreatmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHivTreatmentDAO.GetViewByCode(code, filter.Query());
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
