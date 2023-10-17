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
        internal HIS_ASSESSMENT_MEMBER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAssessmentMemberFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_MEMBER GetByCode(string code, HisAssessmentMemberFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentMemberDAO.GetByCode(code, filter.Query());
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
