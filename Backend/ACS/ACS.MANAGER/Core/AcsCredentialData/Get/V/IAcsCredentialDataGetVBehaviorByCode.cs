using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Get.V
{
    class AcsCredentialDataGetVBehaviorByCode : BeanObjectBase, IAcsCredentialDataGetV
    {
        string code;

        internal AcsCredentialDataGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_CREDENTIAL_DATA IAcsCredentialDataGetV.Run()
        {
            try
            {
                return DAOWorker.AcsCredentialDataDAO.GetViewByCode(code, new AcsCredentialDataViewFilterQuery().Query());
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
