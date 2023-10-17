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
        internal List<V_HIS_IMP_MEST_TYPE_USER> GetView(HisImpMestTypeUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestTypeUserDAO.GetView(filter.Query(), param);
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
