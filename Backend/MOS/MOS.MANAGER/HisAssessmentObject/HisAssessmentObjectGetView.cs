using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    partial class HisAssessmentObjectGet : BusinessBase
    {
        internal List<V_HIS_ASSESSMENT_OBJECT> GetView(HisAssessmentObjectViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.GetView(filter.Query(), param);
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
