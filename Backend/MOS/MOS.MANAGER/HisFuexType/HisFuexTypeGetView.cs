using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeGet : BusinessBase
    {
        internal List<V_HIS_FUEX_TYPE> GetView(HisFuexTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFuexTypeDAO.GetView(filter.Query(), param);
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
