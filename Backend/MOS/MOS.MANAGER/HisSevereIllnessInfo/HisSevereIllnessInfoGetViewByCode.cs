using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoGet : BusinessBase
    {
        internal V_HIS_SEVERE_ILLNESS_INFO GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSevereIllnessInfoViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SEVERE_ILLNESS_INFO GetViewByCode(string code, HisSevereIllnessInfoViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSevereIllnessInfoDAO.GetViewByCode(code, filter.Query());
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
