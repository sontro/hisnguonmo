using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHealthExamRank
{
    partial class HisHealthExamRankDelete : BusinessBase
    {
        internal HisHealthExamRankDelete()
            : base()
        {

        }

        internal HisHealthExamRankDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_HEALTH_EXAM_RANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHealthExamRankCheck checker = new HisHealthExamRankCheck(param);
                valid = valid && IsNotNull(data);
                HIS_HEALTH_EXAM_RANK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisHealthExamRankDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_HEALTH_EXAM_RANK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHealthExamRankCheck checker = new HisHealthExamRankCheck(param);
                List<HIS_HEALTH_EXAM_RANK> listRaw = new List<HIS_HEALTH_EXAM_RANK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisHealthExamRankDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
