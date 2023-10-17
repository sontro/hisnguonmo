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
        internal HIS_EMR_FORM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEmrFormFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_FORM GetByCode(string code, HisEmrFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrFormDAO.GetByCode(code, filter.Query());
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
