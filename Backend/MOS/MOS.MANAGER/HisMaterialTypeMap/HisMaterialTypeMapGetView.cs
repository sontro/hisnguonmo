using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapGet : BusinessBase
    {
        internal List<V_HIS_MATERIAL_TYPE_MAP> GetView(HisMaterialTypeMapViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialTypeMapDAO.GetView(filter.Query(), param);
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
