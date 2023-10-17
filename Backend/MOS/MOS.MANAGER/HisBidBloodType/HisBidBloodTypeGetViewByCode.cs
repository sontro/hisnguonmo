using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidBloodType
{
    partial class HisBidBloodTypeGet : BusinessBase
    {
        internal V_HIS_BID_BLOOD_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBidBloodTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BID_BLOOD_TYPE GetViewByCode(string code, HisBidBloodTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidBloodTypeDAO.GetViewByCode(code, filter.Query());
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
