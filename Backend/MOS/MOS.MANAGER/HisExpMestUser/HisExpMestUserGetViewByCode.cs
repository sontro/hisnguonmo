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
        internal V_HIS_EXP_MEST_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExpMestUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_USER GetViewByCode(string code, HisExpMestUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestUserDAO.GetViewByCode(code, filter.Query());
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
