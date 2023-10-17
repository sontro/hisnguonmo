using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaModuleField.Get.ListV
{
    class SdaModuleFieldGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaModuleFieldGetListV
    {
        SdaModuleFieldViewFilterQuery filterQuery;

        internal SdaModuleFieldGetListVBehaviorByViewFilterQuery(CommonParam param, SdaModuleFieldViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_MODULE_FIELD> ISdaModuleFieldGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.GetView(filterQuery.Query(), param);
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
