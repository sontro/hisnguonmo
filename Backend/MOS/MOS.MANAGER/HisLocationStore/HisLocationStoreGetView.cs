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
        internal List<V_HIS_LOCATION_STORE> GetView(HisLocationStoreViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLocationStoreDAO.GetView(filter.Query(), param);
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
