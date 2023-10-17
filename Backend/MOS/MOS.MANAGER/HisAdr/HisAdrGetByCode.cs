using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrGet : BusinessBase
    {
        internal HIS_ADR GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAdrFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ADR GetByCode(string code, HisAdrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrDAO.GetByCode(code, filter.Query());
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
