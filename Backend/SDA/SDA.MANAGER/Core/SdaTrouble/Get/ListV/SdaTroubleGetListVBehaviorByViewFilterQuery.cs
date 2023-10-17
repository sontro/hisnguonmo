using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTrouble.Get.ListV
{
    class SdaTroubleGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaTroubleGetListV
    {
        SdaTroubleViewFilterQuery filterQuery;

        internal SdaTroubleGetListVBehaviorByViewFilterQuery(CommonParam param, SdaTroubleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_TROUBLE> ISdaTroubleGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaTroubleDAO.GetView(filterQuery.Query(), param);
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
