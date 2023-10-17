using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrGet : BusinessBase
    {
        internal List<V_HIS_ADR> GetView(HisAdrViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ADR GetViewById(long id)
        {
            try
            {
                return DAOWorker.HisAdrDAO.GetViewById(id, new HisAdrViewFilterQuery().Query());
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
