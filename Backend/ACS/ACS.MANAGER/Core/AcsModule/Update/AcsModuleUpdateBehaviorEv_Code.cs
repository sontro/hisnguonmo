using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Update
{
    class AcsModuleUpdateBehaviorEv : BeanObjectBase, IAcsModuleUpdate
    {
        ACS_MODULE entity;

        internal AcsModuleUpdateBehaviorEv(CommonParam param, ACS_MODULE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleDAO.Update(entity);
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
                result = result && AcsModuleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsModuleCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsModuleCheckVerifyExistsCode.Verify(param, entity.MODULE_CODE, entity.ID);
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
