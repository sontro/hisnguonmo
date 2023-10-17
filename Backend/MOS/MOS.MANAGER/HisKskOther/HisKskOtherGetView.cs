using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherGet : BusinessBase
    {
        internal List<V_HIS_KSK_OTHER> GetView(HisKskOtherViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOtherDAO.GetView(filter.Query(), param);
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
