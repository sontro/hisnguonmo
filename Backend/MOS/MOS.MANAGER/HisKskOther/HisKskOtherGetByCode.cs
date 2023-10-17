using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherGet : BusinessBase
    {
        internal HIS_KSK_OTHER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisKskOtherFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OTHER GetByCode(string code, HisKskOtherFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOtherDAO.GetByCode(code, filter.Query());
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
