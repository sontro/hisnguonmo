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
        internal HIS_LICENSE_CLASS GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisLicenseClassFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LICENSE_CLASS GetByCode(string code, HisLicenseClassFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLicenseClassDAO.GetByCode(code, filter.Query());
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
