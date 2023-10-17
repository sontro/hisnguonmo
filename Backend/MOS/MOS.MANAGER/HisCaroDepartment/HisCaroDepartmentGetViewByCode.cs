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
        internal V_HIS_CARO_DEPARTMENT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisCaroDepartmentViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_CARO_DEPARTMENT GetViewByCode(string code, HisCaroDepartmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroDepartmentDAO.GetViewByCode(code, filter.Query());
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
