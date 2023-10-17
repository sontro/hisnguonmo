using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodAbo
{
    partial class HisBloodAboGet : BusinessBase
    {
        internal V_HIS_BLOOD_ABO GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBloodAboViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BLOOD_ABO GetViewByCode(string code, HisBloodAboViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodAboDAO.GetViewByCode(code, filter.Query());
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
