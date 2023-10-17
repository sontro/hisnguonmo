using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLicense.Get.ListV
{
    class SdaLicenseGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaLicenseGetListV
    {
        SdaLicenseViewFilterQuery filterQuery;

        internal SdaLicenseGetListVBehaviorByViewFilterQuery(CommonParam param, SdaLicenseViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_LICENSE> ISdaLicenseGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaLicenseDAO.GetView(filterQuery.Query(), param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
