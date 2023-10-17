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
        internal HIS_IMP_MEST_TYPE_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisImpMestTypeUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_TYPE_USER GetByCode(string code, HisImpMestTypeUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestTypeUserDAO.GetByCode(code, filter.Query());
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
