using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaHideControl.Get.ListV
{
    class SdaHideControlGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaHideControlGetListV
    {
        SdaHideControlViewFilterQuery filterQuery;

        internal SdaHideControlGetListVBehaviorByViewFilterQuery(CommonParam param, SdaHideControlViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_HIDE_CONTROL> ISdaHideControlGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.GetView(filterQuery.Query(), param);
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
