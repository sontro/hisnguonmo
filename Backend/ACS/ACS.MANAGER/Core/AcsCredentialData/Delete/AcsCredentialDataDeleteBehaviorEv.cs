using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData.Delete
{
    class AcsCredentialDataDeleteBehaviorEv : BeanObjectBase, IAcsCredentialDataDelete
    {
        ACS_CREDENTIAL_DATA entity;

        internal AcsCredentialDataDeleteBehaviorEv(CommonParam param, ACS_CREDENTIAL_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsCredentialDataDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsCredentialDataDAO.Truncate(entity);
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
                result = result && AcsCredentialDataCheckVerifyIsUnlock.Verify(param, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
