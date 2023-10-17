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
        internal List<V_HIS_ASSESSMENT_MEMBER> GetView(HisAssessmentMemberViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentMemberDAO.GetView(filter.Query(), param);
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
