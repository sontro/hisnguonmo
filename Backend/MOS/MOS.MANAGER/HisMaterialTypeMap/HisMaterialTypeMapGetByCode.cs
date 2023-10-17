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
        internal HIS_MATERIAL_TYPE_MAP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMaterialTypeMapFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_TYPE_MAP GetByCode(string code, HisMaterialTypeMapFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialTypeMapDAO.GetByCode(code, filter.Query());
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
