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
        internal HIS_ASSESSMENT_OBJECT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAssessmentObjectFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_OBJECT GetByCode(string code, HisAssessmentObjectFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.GetByCode(code, filter.Query());
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
