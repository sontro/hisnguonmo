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
        internal HIS_PACKAGE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPackageFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PACKAGE GetByCode(string code, HisPackageFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDAO.GetByCode(code, filter.Query());
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
