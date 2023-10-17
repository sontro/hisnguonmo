using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaHideControl.Get.ListEv
{
    class SdaHideControlGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaHideControlGetListEv
    {
        SdaHideControlFilterQuery filterQuery;

        internal SdaHideControlGetListEvBehaviorByFilterQuery(CommonParam param, SdaHideControlFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_HIDE_CONTROL> ISdaHideControlGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.Get(filterQuery.Query(), param);
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
