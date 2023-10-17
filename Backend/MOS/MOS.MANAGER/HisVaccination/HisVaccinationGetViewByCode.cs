using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationGet : BusinessBase
    {
        internal V_HIS_VACCINATION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisVaccinationViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_VACCINATION GetViewByCode(string code, HisVaccinationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationDAO.GetViewByCode(code, filter.Query());
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
