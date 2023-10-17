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
        internal List<V_HIS_KSK_SERVICE> GetView(HisKskServiceViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskServiceDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_KSK_SERVICE> GetViewByKskId(long kskId)
        {
            try
            {
                HisKskServiceViewFilterQuery filter = new HisKskServiceViewFilterQuery();
                filter.KSK_ID = kskId;
                return this.GetView(filter);
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
