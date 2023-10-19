using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Update
{
    class AcsApplicationUpdateBehaviorEv : BeanObjectBase, IAcsApplicationUpdate
    {
        ACS_APPLICATION entity;

        internal AcsApplicationUpdateBehaviorEv(CommonParam param, ACS_APPLICATION data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationDAO.Update(entity);
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
                result = result && AcsApplicationCheckVerifyValidData.Verify(param, entity);
                result = result && AcsApplicationCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsApplicationCheckVerifyExistsCode.Verify(param, entity.APPLICATION_CODE, entity.ID);
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
