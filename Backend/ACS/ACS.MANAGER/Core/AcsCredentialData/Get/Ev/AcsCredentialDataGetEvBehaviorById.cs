using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.Ev
{
    class AcsCredentialDataGetEvBehaviorById : BeanObjectBase, IAcsCredentialDataGetEv
    {
        long id;

        internal AcsCredentialDataGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_CREDENTIAL_DATA IAcsCredentialDataGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.GetById(id, new AcsCredentialDataFilterQuery().Query());
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
