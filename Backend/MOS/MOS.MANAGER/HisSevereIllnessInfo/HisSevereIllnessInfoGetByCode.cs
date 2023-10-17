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
        internal HIS_SEVERE_ILLNESS_INFO GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSevereIllnessInfoFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SEVERE_ILLNESS_INFO GetByCode(string code, HisSevereIllnessInfoFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSevereIllnessInfoDAO.GetByCode(code, filter.Query());
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
