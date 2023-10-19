using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType.Delete
{
    class AcsActivityTypeDeleteBehaviorEv : BeanObjectBase, IAcsActivityTypeDelete
    {
        ACS_ACTIVITY_TYPE entity;

        internal AcsActivityTypeDeleteBehaviorEv(CommonParam param, ACS_ACTIVITY_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsActivityTypeDAO.Truncate(entity);
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
                result = result && AcsActivityTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
