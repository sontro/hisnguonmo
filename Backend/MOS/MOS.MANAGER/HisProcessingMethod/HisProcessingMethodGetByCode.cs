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
        internal HIS_PROCESSING_METHOD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisProcessingMethodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PROCESSING_METHOD GetByCode(string code, HisProcessingMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.GetByCode(code, filter.Query());
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
