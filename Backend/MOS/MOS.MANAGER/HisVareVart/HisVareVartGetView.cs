using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVareVart
{
    partial class HisVareVartGet : BusinessBase
    {
        internal List<V_HIS_VARE_VART> GetView(HisVareVartViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVareVartDAO.GetView(filter.Query(), param);
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
