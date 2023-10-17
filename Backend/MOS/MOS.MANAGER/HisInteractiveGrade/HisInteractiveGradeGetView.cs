using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    partial class HisInteractiveGradeGet : BusinessBase
    {
        internal List<V_HIS_INTERACTIVE_GRADE> GetView(HisInteractiveGradeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInteractiveGradeDAO.GetView(filter.Query(), param);
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
