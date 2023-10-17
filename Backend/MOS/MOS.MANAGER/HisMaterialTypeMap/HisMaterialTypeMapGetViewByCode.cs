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
        internal V_HIS_MATERIAL_TYPE_MAP GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMaterialTypeMapViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MATERIAL_TYPE_MAP GetViewByCode(string code, HisMaterialTypeMapViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialTypeMapDAO.GetViewByCode(code, filter.Query());
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
