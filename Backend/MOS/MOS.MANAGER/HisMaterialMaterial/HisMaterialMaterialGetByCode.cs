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
        internal HIS_MATERIAL_MATERIAL GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMaterialMaterialFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_MATERIAL GetByCode(string code, HisMaterialMaterialFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialMaterialDAO.GetByCode(code, filter.Query());
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
