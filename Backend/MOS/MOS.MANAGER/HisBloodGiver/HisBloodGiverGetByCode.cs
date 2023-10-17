using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverGet : BusinessBase
    {
        internal HIS_BLOOD_GIVER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBloodGiverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLOOD_GIVER GetByCode(string code, HisBloodGiverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodGiverDAO.GetByCode(code, filter.Query());
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
