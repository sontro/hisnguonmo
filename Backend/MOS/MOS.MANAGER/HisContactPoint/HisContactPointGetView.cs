using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointGet : BusinessBase
    {
        internal List<V_HIS_CONTACT_POINT> GetView(HisContactPointViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactPointDAO.GetView(filter.Query(), param);
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
