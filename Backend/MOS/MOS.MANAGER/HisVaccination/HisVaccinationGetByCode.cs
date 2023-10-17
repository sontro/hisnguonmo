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
        internal HIS_VACCINATION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccinationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION GetByCode(string code, HisVaccinationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationDAO.GetByCode(code, filter.Query());
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
