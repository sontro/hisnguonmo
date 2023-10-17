using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBank
{
    partial class HisBankGet : BusinessBase
    {
        internal HIS_BANK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBankFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BANK GetByCode(string code, HisBankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBankDAO.GetByCode(code, filter.Query());
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
