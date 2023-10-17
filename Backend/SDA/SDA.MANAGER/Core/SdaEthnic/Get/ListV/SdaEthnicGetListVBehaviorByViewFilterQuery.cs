using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEthnic.Get.ListV
{
    class SdaEthnicGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaEthnicGetListV
    {
        SdaEthnicViewFilterQuery filterQuery;

        internal SdaEthnicGetListVBehaviorByViewFilterQuery(CommonParam param, SdaEthnicViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_ETHNIC> ISdaEthnicGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaEthnicDAO.GetView(filterQuery.Query(), param);
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
