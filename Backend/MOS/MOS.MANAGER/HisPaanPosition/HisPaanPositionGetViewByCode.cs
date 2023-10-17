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
        internal V_HIS_PAAN_POSITION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPaanPositionViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PAAN_POSITION GetViewByCode(string code, HisPaanPositionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPaanPositionDAO.GetViewByCode(code, filter.Query());
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
