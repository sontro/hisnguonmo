using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCommuneMap.Get.ListEv
{
    class SdaCommuneMapGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaCommuneMapGetListEv
    {
        SdaCommuneMapFilterQuery filterQuery;

        internal SdaCommuneMapGetListEvBehaviorByFilterQuery(CommonParam param, SdaCommuneMapFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_COMMUNE_MAP> ISdaCommuneMapGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaCommuneMapDAO.Get(filterQuery.Query(), param);
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
