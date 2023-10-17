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
        internal HIS_BHYT_PARAM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBhytParamFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BHYT_PARAM GetByCode(string code, HisBhytParamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytParamDAO.GetByCode(code, filter.Query());
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
