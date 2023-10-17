using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceGet : BusinessBase
    {
        internal HIS_KSK_SERVICE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisKskServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_SERVICE GetByCode(string code, HisKskServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskServiceDAO.GetByCode(code, filter.Query());
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
