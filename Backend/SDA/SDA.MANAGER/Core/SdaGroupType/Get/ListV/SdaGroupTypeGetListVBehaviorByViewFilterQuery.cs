using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroupType.Get.ListV
{
    class SdaGroupTypeGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaGroupTypeGetListV
    {
        SdaGroupTypeViewFilterQuery filterQuery;

        internal SdaGroupTypeGetListVBehaviorByViewFilterQuery(CommonParam param, SdaGroupTypeViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_GROUP_TYPE> ISdaGroupTypeGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaGroupTypeDAO.GetView(filterQuery.Query(), param);
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
