using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkip
{
    partial class HisEkipGet : BusinessBase
    {
        internal HIS_EKIP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEkipFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP GetByCode(string code, HisEkipFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipDAO.GetByCode(code, filter.Query());
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
