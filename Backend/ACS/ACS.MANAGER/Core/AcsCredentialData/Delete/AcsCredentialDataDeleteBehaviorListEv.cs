using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Delete
{
    class AcsCredentialDataDeleteBehaviorListEv : BeanObjectBase, IAcsCredentialDataDelete
    {
        List<ACS_CREDENTIAL_DATA> entitys;

        internal AcsCredentialDataDeleteBehaviorListEv(CommonParam param, List<ACS_CREDENTIAL_DATA> data)
            : base(param)
        {
            entitys = data;
        }

        bool IAcsCredentialDataDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsCredentialDataDAO.TruncateList(entitys);
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
                result = true;
                //foreach (var entity in entitys)
                //{
                //    result = result && AcsCredentialDataCheckVerifyIsUnlock.Verify(param, entity.ID);
                //}
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
