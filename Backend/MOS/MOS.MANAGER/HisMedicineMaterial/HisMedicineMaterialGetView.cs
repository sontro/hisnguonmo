using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    partial class HisMedicineMaterialGet : BusinessBase
    {
        internal List<V_HIS_MEDICINE_MATERIAL> GetView(HisMedicineMaterialViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMaterialDAO.GetView(filter.Query(), param);
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
