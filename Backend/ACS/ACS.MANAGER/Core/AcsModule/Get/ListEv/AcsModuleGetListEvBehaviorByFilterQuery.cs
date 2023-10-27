using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule.Get.ListEv
{
    class AcsModuleGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsModuleGetListEv
    {
        AcsModuleFilterQuery filterQuery;

        internal AcsModuleGetListEvBehaviorByFilterQuery(CommonParam param, AcsModuleFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_MODULE> IAcsModuleGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.Get(filterQuery.Query(), param);
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
