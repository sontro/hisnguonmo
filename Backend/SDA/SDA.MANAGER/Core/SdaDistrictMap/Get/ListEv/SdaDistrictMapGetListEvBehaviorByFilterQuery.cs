using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDistrictMap.Get.ListEv
{
    class SdaDistrictMapGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaDistrictMapGetListEv
    {
        SdaDistrictMapFilterQuery filterQuery;

        internal SdaDistrictMapGetListEvBehaviorByFilterQuery(CommonParam param, SdaDistrictMapFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_DISTRICT_MAP> ISdaDistrictMapGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaDistrictMapDAO.Get(filterQuery.Query(), param);
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
