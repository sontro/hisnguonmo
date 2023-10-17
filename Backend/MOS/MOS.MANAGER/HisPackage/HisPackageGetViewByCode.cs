using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackage
{
    partial class HisPackageGet : BusinessBase
    {
        internal V_HIS_PACKAGE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPackageViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PACKAGE GetViewByCode(string code, HisPackageViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDAO.GetViewByCode(code, filter.Query());
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
