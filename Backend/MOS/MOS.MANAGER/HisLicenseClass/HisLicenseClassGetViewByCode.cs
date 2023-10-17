using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassGet : BusinessBase
    {
        internal V_HIS_LICENSE_CLASS GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisLicenseClassViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_LICENSE_CLASS GetViewByCode(string code, HisLicenseClassViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLicenseClassDAO.GetViewByCode(code, filter.Query());
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
