using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestUser
{
    partial class HisImpMestUserGet : BusinessBase
    {
        internal V_HIS_IMP_MEST_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisImpMestUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_USER GetViewByCode(string code, HisImpMestUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestUserDAO.GetViewByCode(code, filter.Query());
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
