using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestUser
{
    partial class HisExpMestUserGet : BusinessBase
    {
        internal HIS_EXP_MEST_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisExpMestUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXP_MEST_USER GetByCode(string code, HisExpMestUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestUserDAO.GetByCode(code, filter.Query());
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
