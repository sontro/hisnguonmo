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
        internal V_HIS_ASSESSMENT_OBJECT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAssessmentObjectViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ASSESSMENT_OBJECT GetViewByCode(string code, HisAssessmentObjectViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.GetViewByCode(code, filter.Query());
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
