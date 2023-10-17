using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : BusinessBase
    {
        internal V_HIS_SKIN_SURGERY_DESC GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSkinSurgeryDescViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SKIN_SURGERY_DESC GetViewByCode(string code, HisSkinSurgeryDescViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSkinSurgeryDescDAO.GetViewByCode(code, filter.Query());
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
