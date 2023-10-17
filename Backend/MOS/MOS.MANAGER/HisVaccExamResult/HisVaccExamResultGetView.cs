using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccExamResult
{
    partial class HisVaccExamResultGet : BusinessBase
    {
        internal List<V_HIS_VACC_EXAM_RESULT> GetView(HisVaccExamResultViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccExamResultDAO.GetView(filter.Query(), param);
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
