using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAtc
{
    partial class HisAtcGet : BusinessBase
    {
        internal HIS_ATC GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAtcFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ATC GetByCode(string code, HisAtcFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAtcDAO.GetByCode(code, filter.Query());
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
