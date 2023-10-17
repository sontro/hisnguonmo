using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeGet : BusinessBase
    {
        internal HIS_BID_MATERIAL_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBidMaterialTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BID_MATERIAL_TYPE GetByCode(string code, HisBidMaterialTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBidMaterialTypeDAO.GetByCode(code, filter.Query());
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
