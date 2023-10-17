using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserGet : BusinessBase
    {
        internal V_HIS_IMP_MEST_TYPE_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpMestTypeUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_TYPE_USER GetViewByCode(string code, HisImpMestTypeUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestTypeUserDAO.GetViewByCode(code, filter.Query());
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
