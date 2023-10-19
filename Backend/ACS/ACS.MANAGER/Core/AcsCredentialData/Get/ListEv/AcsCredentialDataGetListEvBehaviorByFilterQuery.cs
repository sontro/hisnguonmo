using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.ListEv
{
    class AcsCredentialDataGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsCredentialDataGetListEv
    {
        AcsCredentialDataFilterQuery filterQuery;

        internal AcsCredentialDataGetListEvBehaviorByFilterQuery(CommonParam param, AcsCredentialDataFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_CREDENTIAL_DATA> IAcsCredentialDataGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.Get(filterQuery.Query(), param);
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
