using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempGet : BusinessBase
    {
        internal List<V_HIS_TRAN_PATI_TEMP> GetView(HisTranPatiTempViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTempDAO.GetView(filter.Query(), param);
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
