using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Update
{
    class AcsRoleBaseUpdateBehaviorEv : BeanObjectBase, IAcsRoleBaseUpdate
    {
        ACS_ROLE_BASE entity;

        internal AcsRoleBaseUpdateBehaviorEv(CommonParam param, ACS_ROLE_BASE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleBaseUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleBaseDAO.Update(entity);
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
                result = result && AcsRoleBaseCheckVerifyValidData.Verify(param, entity);
                result = result && AcsRoleBaseCheckVerifyIsUnlock.Verify(param, entity.ID);
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
