using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    partial class HisLocationStoreGet : BusinessBase
    {
        internal V_HIS_LOCATION_STORE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisLocationStoreViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_LOCATION_STORE GetViewByCode(string code, HisLocationStoreViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLocationStoreDAO.GetViewByCode(code, filter.Query());
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
