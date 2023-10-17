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
        internal List<V_HIS_LICENSE_CLASS> GetView(HisLicenseClassViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLicenseClassDAO.GetView(filter.Query(), param);
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
