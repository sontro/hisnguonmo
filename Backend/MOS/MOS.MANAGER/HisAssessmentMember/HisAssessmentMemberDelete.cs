using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAssessmentMember
{
    partial class HisAssessmentMemberDelete : BusinessBase
    {
        internal HisAssessmentMemberDelete()
            : base()
        {

        }

        internal HisAssessmentMemberDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ASSESSMENT_MEMBER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentMemberCheck checker = new HisAssessmentMemberCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_MEMBER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAssessmentMemberDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ASSESSMENT_MEMBER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAssessmentMemberCheck checker = new HisAssessmentMemberCheck(param);
                List<HIS_ASSESSMENT_MEMBER> listRaw = new List<HIS_ASSESSMENT_MEMBER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAssessmentMemberDAO.DeleteList(listData);
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
