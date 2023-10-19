using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.Ev
{
    class AcsCredentialDataGetEvBehaviorByTokenManagerFilter : BeanObjectBase, IAcsCredentialDataGetEv
    {
        AcsCredentialDataFilterForTokenManager entity;

        internal AcsCredentialDataGetEvBehaviorByTokenManagerFilter(CommonParam param, AcsCredentialDataFilterForTokenManager filter)
            : base(param)
        {
            entity = filter;
        }

        ACS_CREDENTIAL_DATA IAcsCredentialDataGetEv.Run()
        {
            try
            {
                AcsCredentialDataFilterQuery filterQuery = new AcsCredentialDataFilterQuery();
                filterQuery.RESOURCE_SYSTEM_CODE = entity.RESOURCE_SYSTEM_CODE;
                filterQuery.TOKEN_CODE = entity.TOKEN_CODE;
                filterQuery.DATA_KEY = entity.DATA_KEY;
                filterQuery.IS_ACTIVE = entity.IS_ACTIVE;

                return DAOWorker.AcsCredentialDataDAO.Get(filterQuery.Query(), param).SingleOrDefault();
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
