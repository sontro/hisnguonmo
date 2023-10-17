using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamGet : BusinessBase
    {
        internal V_HIS_BHYT_PARAM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBhytParamViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BHYT_PARAM GetViewByCode(string code, HisBhytParamViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytParamDAO.GetViewByCode(code, filter.Query());
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
