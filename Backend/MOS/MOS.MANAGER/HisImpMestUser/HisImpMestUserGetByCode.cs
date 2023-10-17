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
        internal HIS_IMP_MEST_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisImpMestUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_USER GetByCode(string code, HisImpMestUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestUserDAO.GetByCode(code, filter.Query());
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
