using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType.Create
{
    class AcsActivityTypeCreateBehaviorEv : BeanObjectBase, IAcsActivityTypeCreate
    {
        ACS_ACTIVITY_TYPE entity;

        internal AcsActivityTypeCreateBehaviorEv(CommonParam param, ACS_ACTIVITY_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsActivityTypeDAO.Create(entity);
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
                result = result && AcsActivityTypeCheckVerifyValidData.Verify(param, entity);
                result = result && AcsActivityTypeCheckVerifyExistsCode.Verify(param, entity.ACTIVITY_TYPE_CODE, null);
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
