using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    partial class HisMaterialMaterialGet : BusinessBase
    {
        internal List<V_HIS_MATERIAL_MATERIAL> GetView(HisMaterialMaterialViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialMaterialDAO.GetView(filter.Query(), param);
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
