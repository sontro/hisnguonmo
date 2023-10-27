using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Create
{
    class AcsModuleCreateBehaviorEv : BeanObjectBase, IAcsModuleCreate
    {
        ACS_MODULE entity;

        internal AcsModuleCreateBehaviorEv(CommonParam param, ACS_MODULE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleDAO.Create(entity);
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
                result = result && AcsModuleCheckVerifyExistsCode.Verify(param, entity.MODULE_CODE, null);
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
