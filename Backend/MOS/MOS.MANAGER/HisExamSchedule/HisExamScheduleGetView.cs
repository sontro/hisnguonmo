using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleGet : BusinessBase
    {
        internal List<V_HIS_EXAM_SCHEDULE> GetView(HisExamScheduleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExamScheduleDAO.GetView(filter.Query(), param);
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
