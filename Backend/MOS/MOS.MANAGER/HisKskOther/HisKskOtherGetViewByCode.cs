using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherGet : BusinessBase
    {
        internal V_HIS_KSK_OTHER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisKskOtherViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_OTHER GetViewByCode(string code, HisKskOtherViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOtherDAO.GetViewByCode(code, filter.Query());
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
