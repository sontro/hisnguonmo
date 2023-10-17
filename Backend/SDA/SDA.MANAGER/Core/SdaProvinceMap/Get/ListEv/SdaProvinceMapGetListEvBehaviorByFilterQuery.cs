using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaProvinceMap.Get.ListEv
{
    class SdaProvinceMapGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaProvinceMapGetListEv
    {
        SdaProvinceMapFilterQuery filterQuery;

        internal SdaProvinceMapGetListEvBehaviorByFilterQuery(CommonParam param, SdaProvinceMapFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_PROVINCE_MAP> ISdaProvinceMapGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaProvinceMapDAO.Get(filterQuery.Query(), param);
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
