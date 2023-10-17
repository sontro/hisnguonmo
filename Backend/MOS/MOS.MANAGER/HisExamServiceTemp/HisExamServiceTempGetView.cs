using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamServiceTemp
{
    partial class HisExamServiceTempGet : BusinessBase
    {
        internal List<V_HIS_EXAM_SERVICE_TEMP> GetView(HisExamServiceTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExamServiceTempDAO.GetView(filter.Query(), param);
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
