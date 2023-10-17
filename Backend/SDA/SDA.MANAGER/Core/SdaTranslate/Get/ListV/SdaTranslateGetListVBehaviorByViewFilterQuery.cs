using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTranslate.Get.ListV
{
    class SdaTranslateGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaTranslateGetListV
    {
        SdaTranslateViewFilterQuery filterQuery;

        internal SdaTranslateGetListVBehaviorByViewFilterQuery(CommonParam param, SdaTranslateViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_TRANSLATE> ISdaTranslateGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaTranslateDAO.GetView(filterQuery.Query(), param);
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
