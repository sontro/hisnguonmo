using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodAbo
{
    partial class HisBloodAboGet : BusinessBase
    {
        internal List<V_HIS_BLOOD_ABO> GetView(HisBloodAboViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBloodAboDAO.GetView(filter.Query(), param);
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
