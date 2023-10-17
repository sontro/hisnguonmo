using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverGet : BusinessBase
    {
        internal HIS_KSK_DRIVER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisKskDriverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_DRIVER GetByCode(string code, HisKskDriverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.GetByCode(code, filter.Query());
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
