using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodGet : BusinessBase
    {
        internal V_HIS_PROCESSING_METHOD GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisProcessingMethodViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PROCESSING_METHOD GetViewByCode(string code, HisProcessingMethodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.GetViewByCode(code, filter.Query());
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
