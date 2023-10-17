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
        internal List<V_HIS_PACKAGE> GetView(HisPackageViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDAO.GetView(filter.Query(), param);
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
