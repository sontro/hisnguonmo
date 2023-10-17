using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaModuleField.Get.ListEv
{
    class SdaModuleFieldGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaModuleFieldGetListEv
    {
        SdaModuleFieldFilterQuery filterQuery;

        internal SdaModuleFieldGetListEvBehaviorByFilterQuery(CommonParam param, SdaModuleFieldFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_MODULE_FIELD> ISdaModuleFieldGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.Get(filterQuery.Query(), param);
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
