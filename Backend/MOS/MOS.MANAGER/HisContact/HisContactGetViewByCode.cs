using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    partial class HisContactGet : BusinessBase
    {
        internal V_HIS_CONTACT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisContactViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_CONTACT GetViewByCode(string code, HisContactViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactDAO.GetViewByCode(code, filter.Query());
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
