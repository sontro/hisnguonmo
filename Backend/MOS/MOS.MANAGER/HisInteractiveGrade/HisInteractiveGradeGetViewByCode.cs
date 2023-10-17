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
        internal V_HIS_INTERACTIVE_GRADE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisInteractiveGradeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_INTERACTIVE_GRADE GetViewByCode(string code, HisInteractiveGradeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInteractiveGradeDAO.GetViewByCode(code, filter.Query());
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
