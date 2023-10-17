using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    partial class HisPtttTableGet : BusinessBase
    {
        internal List<V_HIS_PTTT_TABLE> GetView(HisPtttTableViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttTableDAO.GetView(filter.Query(), param);
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
