using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanPosition
{
    partial class HisPaanPositionGet : BusinessBase
    {
        internal List<V_HIS_PAAN_POSITION> GetView(HisPaanPositionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPaanPositionDAO.GetView(filter.Query(), param);
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
