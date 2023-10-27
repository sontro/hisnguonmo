using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Create
{
    class AcsCredentialDataCreateBehaviorEv : BeanObjectBase, IAcsCredentialDataCreate
    {
        ACS_CREDENTIAL_DATA entity;

        internal AcsCredentialDataCreateBehaviorEv(CommonParam param, ACS_CREDENTIAL_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsCredentialDataCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsCredentialDataDAO.Create(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsCredentialDataCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
