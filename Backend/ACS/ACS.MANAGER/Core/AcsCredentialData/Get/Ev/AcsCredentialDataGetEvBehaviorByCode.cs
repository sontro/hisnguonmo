using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.Ev
{
    class AcsCredentialDataGetEvBehaviorByCode : BeanObjectBase, IAcsCredentialDataGetEv
    {
        string code;

        internal AcsCredentialDataGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_CREDENTIAL_DATA IAcsCredentialDataGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.GetByCode(code, new AcsCredentialDataFilterQuery().Query());
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
