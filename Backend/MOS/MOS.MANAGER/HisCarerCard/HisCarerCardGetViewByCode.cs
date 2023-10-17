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
        internal V_HIS_CARER_CARD GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisCarerCardViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_CARER_CARD GetViewByCode(string code, HisCarerCardViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardDAO.GetViewByCode(code, filter.Query());
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
