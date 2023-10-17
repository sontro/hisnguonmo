using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttGet : BusinessBase
    {
        internal V_HIS_VACCINATION_STT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisVaccinationSttViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_VACCINATION_STT GetViewByCode(string code, HisVaccinationSttViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationSttDAO.GetViewByCode(code, filter.Query());
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
