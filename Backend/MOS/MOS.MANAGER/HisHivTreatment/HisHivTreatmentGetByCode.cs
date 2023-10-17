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
        internal HIS_HIV_TREATMENT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisHivTreatmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HIV_TREATMENT GetByCode(string code, HisHivTreatmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHivTreatmentDAO.GetByCode(code, filter.Query());
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
