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
        internal HIS_CARER_CARD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisCarerCardFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARER_CARD GetByCode(string code, HisCarerCardFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardDAO.GetByCode(code, filter.Query());
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
