using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    partial class HisPackageDetailGet : BusinessBase
    {
        internal List<V_HIS_PACKAGE_DETAIL> GetView(HisPackageDetailViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDetailDAO.GetView(filter.Query(), param);
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
