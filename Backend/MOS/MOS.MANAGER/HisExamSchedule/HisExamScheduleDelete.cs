using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleDelete : BusinessBase
    {
        internal HisExamScheduleDelete()
            : base()
        {

        }

        internal HisExamScheduleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXAM_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXAM_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExamScheduleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXAM_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                List<HIS_EXAM_SCHEDULE> listRaw = new List<HIS_EXAM_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExamScheduleDAO.DeleteList(listData);
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
