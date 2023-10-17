using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaReligion.Get.ListV
{
    class SdaReligionGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaReligionGetListV
    {
        SdaReligionViewFilterQuery filterQuery;

        internal SdaReligionGetListVBehaviorByViewFilterQuery(CommonParam param, SdaReligionViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_RELIGION> ISdaReligionGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaReligionDAO.GetView(filterQuery.Query(), param);
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
