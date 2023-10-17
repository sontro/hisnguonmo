using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKsk
{
    partial class HisKskGet : BusinessBase
    {
        internal HIS_KSK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisKskFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK GetByCode(string code, HisKskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDAO.GetByCode(code, filter.Query());
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
