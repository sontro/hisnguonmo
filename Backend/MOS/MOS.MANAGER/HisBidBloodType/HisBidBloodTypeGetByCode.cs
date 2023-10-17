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
        internal HIS_BID_BLOOD_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBidBloodTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BID_BLOOD_TYPE GetByCode(string code, HisBidBloodTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidBloodTypeDAO.GetByCode(code, filter.Query());
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
