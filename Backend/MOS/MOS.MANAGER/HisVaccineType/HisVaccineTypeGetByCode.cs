using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccineType
{
    partial class HisVaccineTypeGet : BusinessBase
    {
        internal HIS_VACCINE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccineTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINE_TYPE GetByCode(string code, HisVaccineTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccineTypeDAO.GetByCode(code, filter.Query());
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
