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
        internal HIS_VACCINATION_STT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccinationSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION_STT GetByCode(string code, HisVaccinationSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationSttDAO.GetByCode(code, filter.Query());
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
