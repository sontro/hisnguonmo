using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    partial class HisEmrFormGet : BusinessBase
    {
        internal V_HIS_EMR_FORM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEmrFormViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EMR_FORM GetViewByCode(string code, HisEmrFormViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrFormDAO.GetViewByCode(code, filter.Query());
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
