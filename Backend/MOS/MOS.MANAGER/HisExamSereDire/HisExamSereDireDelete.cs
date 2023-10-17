using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSereDire
{
    partial class HisExamSereDireDelete : BusinessBase
    {
        internal HisExamSereDireDelete()
            : base()
        {

        }

        internal HisExamSereDireDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXAM_SERE_DIRE data)
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
                    result = DAOWorker.HisExamSereDireDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXAM_SERE_DIRE> listData)
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
                    result = DAOWorker.HisExamSereDireDAO.DeleteList(listData);
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
