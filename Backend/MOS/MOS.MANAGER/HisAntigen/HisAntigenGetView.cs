using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenGet : BusinessBase
    {
        internal List<V_HIS_ANTIGEN> GetView(HisAntigenViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenDAO.GetView(filter.Query(), param);
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
