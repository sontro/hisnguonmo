using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHealthExamRank
{
    partial class HisHealthExamRankGet : BusinessBase
    {
        internal HIS_HEALTH_EXAM_RANK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisHealthExamRankFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HEALTH_EXAM_RANK GetByCode(string code, HisHealthExamRankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHealthExamRankDAO.GetByCode(code, filter.Query());
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
