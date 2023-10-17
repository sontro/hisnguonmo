using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatySub
{
    partial class HisMestPatySubGet : BusinessBase
    {
        internal HIS_MEST_PATY_SUB GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMestPatySubFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_PATY_SUB GetByCode(string code, HisMestPatySubFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPatySubDAO.GetByCode(code, filter.Query());
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
