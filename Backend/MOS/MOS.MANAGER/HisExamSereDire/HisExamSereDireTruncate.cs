using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSereDire
{
    partial class HisExamSereDireTruncate : BusinessBase
    {
        internal HisExamSereDireTruncate()
            : base()
        {

        }

        internal HisExamSereDireTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EXAM_SERE_DIRE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXAM_SERE_DIRE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExamSereDireDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                List<HIS_EXAM_SERE_DIRE> listRaw = new List<HIS_EXAM_SERE_DIRE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExamSereDireDAO.TruncateList(listData);
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

        internal bool TruncateByServiceReqId(long serviceReqId)
        {
            bool result = false;
            try
            {
                HisExamSereDireFilterQuery filter = new HisExamSereDireFilterQuery();
                filter.SERVICE_REQ_ID = serviceReqId;
                List<HIS_EXAM_SERE_DIRE> listData = new HisExamSereDireGet().Get(filter);
                result = (!IsNotNullOrEmpty(listData) || DAOWorker.HisExamSereDireDAO.TruncateList(listData));
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
