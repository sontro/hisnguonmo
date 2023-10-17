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
        internal V_HIS_BLOOD_GIVER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBloodGiverViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BLOOD_GIVER GetViewByCode(string code, HisBloodGiverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodGiverDAO.GetViewByCode(code, filter.Query());
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
