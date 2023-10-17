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
        internal HIS_INTERACTIVE_GRADE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisInteractiveGradeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_INTERACTIVE_GRADE GetByCode(string code, HisInteractiveGradeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInteractiveGradeDAO.GetByCode(code, filter.Query());
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
