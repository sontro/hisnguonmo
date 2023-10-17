using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailGet : BusinessBase
    {
        internal List<V_HIS_SURG_REMU_DETAIL> GetView(HisSurgRemuDetailViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemuDetailDAO.GetView(filter.Query(), param);
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
