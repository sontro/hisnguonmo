using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    partial class HisMemaGroupGet : BusinessBase
    {
        internal V_HIS_MEMA_GROUP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMemaGroupViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEMA_GROUP GetViewByCode(string code, HisMemaGroupViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMemaGroupDAO.GetViewByCode(code, filter.Query());
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
