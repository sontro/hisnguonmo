using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeciality
{
    partial class HisSpecialityGet : BusinessBase
    {
        internal HIS_SPECIALITY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSpecialityFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPECIALITY GetByCode(string code, HisSpecialityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpecialityDAO.GetByCode(code, filter.Query());
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
