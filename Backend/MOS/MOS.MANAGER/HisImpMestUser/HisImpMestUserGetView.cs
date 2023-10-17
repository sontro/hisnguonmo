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
        internal List<V_HIS_IMP_MEST_USER> GetView(HisImpMestUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestUserDAO.GetView(filter.Query(), param);
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
