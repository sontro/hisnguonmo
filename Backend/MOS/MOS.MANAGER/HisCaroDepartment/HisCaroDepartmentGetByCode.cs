using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentGet : BusinessBase
    {
        internal HIS_CARO_DEPARTMENT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisCaroDepartmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARO_DEPARTMENT GetByCode(string code, HisCaroDepartmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroDepartmentDAO.GetByCode(code, filter.Query());
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
