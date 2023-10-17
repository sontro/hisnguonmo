using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProgram
{
    partial class HisProgramGet : BusinessBase
    {
        internal List<V_HIS_PROGRAM> GetView(HisProgramViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProgramDAO.GetView(filter.Query(), param);
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
