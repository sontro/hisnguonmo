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
        internal HIS_FUEX_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisFuexTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FUEX_TYPE GetByCode(string code, HisFuexTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFuexTypeDAO.GetByCode(code, filter.Query());
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
