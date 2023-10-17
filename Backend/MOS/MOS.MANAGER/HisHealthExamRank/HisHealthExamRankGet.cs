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
        internal HisHealthExamRankGet()
            : base()
        {

        }

        internal HisHealthExamRankGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HEALTH_EXAM_RANK> Get(HisHealthExamRankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHealthExamRankDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HEALTH_EXAM_RANK GetById(long id)
        {
            try
            {
                return GetById(id, new HisHealthExamRankFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HEALTH_EXAM_RANK GetById(long id, HisHealthExamRankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHealthExamRankDAO.GetById(id, filter.Query());
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
