using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestBlood
{
    partial class HisImpMestBloodGet : BusinessBase
    {
        internal List<V_HIS_IMP_MEST_BLOOD> GetView(HisImpMestBloodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestBloodDAO.GetView(filter.Query(), param);
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
