using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentMember
{
    partial class HisAssessmentMemberGet : BusinessBase
    {
        internal V_HIS_ASSESSMENT_MEMBER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAssessmentMemberViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ASSESSMENT_MEMBER GetViewByCode(string code, HisAssessmentMemberViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentMemberDAO.GetViewByCode(code, filter.Query());
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
