using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamServiceTemp
{
    partial class HisExamServiceTempDelete : BusinessBase
    {
        internal HisExamServiceTempDelete()
            : base()
        {

        }

        internal HisExamServiceTempDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXAM_SERVICE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamServiceTempCheck checker = new HisExamServiceTempCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXAM_SERVICE_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExamServiceTempDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXAM_SERVICE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamServiceTempCheck checker = new HisExamServiceTempCheck(param);
                List<HIS_EXAM_SERVICE_TEMP> listRaw = new List<HIS_EXAM_SERVICE_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExamServiceTempDAO.DeleteList(listData);
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
