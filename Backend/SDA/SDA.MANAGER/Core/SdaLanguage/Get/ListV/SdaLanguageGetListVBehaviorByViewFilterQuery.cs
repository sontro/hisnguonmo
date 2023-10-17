using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLanguage.Get.ListV
{
    class SdaLanguageGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaLanguageGetListV
    {
        SdaLanguageViewFilterQuery filterQuery;

        internal SdaLanguageGetListVBehaviorByViewFilterQuery(CommonParam param, SdaLanguageViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_LANGUAGE> ISdaLanguageGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaLanguageDAO.GetView(filterQuery.Query(), param);
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
