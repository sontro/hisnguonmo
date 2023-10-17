using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    partial class HisMestPatyTrtyGet : BusinessBase
    {
        internal List<V_HIS_MEST_PATY_TRTY> GetView(HisMestPatyTrtyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPatyTrtyDAO.GetView(filter.Query(), param);
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
