using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardGet : BusinessBase
    {
        internal List<V_HIS_CARER_CARD> GetView(HisCarerCardViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardDAO.GetView(filter.Query(), param);
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
