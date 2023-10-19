using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.V
{
    class AcsCredentialDataGetVBehaviorById : BeanObjectBase, IAcsCredentialDataGetV
    {
        long id;

        internal AcsCredentialDataGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_CREDENTIAL_DATA IAcsCredentialDataGetV.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.GetViewById(id, new AcsCredentialDataViewFilterQuery().Query());
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
