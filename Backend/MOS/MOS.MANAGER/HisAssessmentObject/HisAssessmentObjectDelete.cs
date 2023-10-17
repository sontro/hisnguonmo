using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAssessmentObject
{
    partial class HisAssessmentObjectDelete : BusinessBase
    {
        internal HisAssessmentObjectDelete()
            : base()
        {

        }

        internal HisAssessmentObjectDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ASSESSMENT_OBJECT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentObjectCheck checker = new HisAssessmentObjectCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_OBJECT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAssessmentObjectDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ASSESSMENT_OBJECT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAssessmentObjectCheck checker = new HisAssessmentObjectCheck(param);
                List<HIS_ASSESSMENT_OBJECT> listRaw = new List<HIS_ASSESSMENT_OBJECT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAssessmentObjectDAO.DeleteList(listData);
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
