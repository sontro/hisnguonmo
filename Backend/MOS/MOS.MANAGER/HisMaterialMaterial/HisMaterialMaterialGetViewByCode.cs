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
        internal V_HIS_MATERIAL_MATERIAL GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMaterialMaterialViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MATERIAL_MATERIAL GetViewByCode(string code, HisMaterialMaterialViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialMaterialDAO.GetViewByCode(code, filter.Query());
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
