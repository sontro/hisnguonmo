using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeGet : BusinessBase
    {
        internal V_HIS_FUEX_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisFuexTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_FUEX_TYPE GetViewByCode(string code, HisFuexTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFuexTypeDAO.GetViewByCode(code, filter.Query());
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
