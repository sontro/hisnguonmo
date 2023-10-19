using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.ListV
{
    class AcsCredentialDataGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsCredentialDataGetListV
    {
        AcsCredentialDataViewFilterQuery filterQuery;

        internal AcsCredentialDataGetListVBehaviorByViewFilterQuery(CommonParam param, AcsCredentialDataViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_CREDENTIAL_DATA> IAcsCredentialDataGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.GetView(filterQuery.Query(), param);
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
