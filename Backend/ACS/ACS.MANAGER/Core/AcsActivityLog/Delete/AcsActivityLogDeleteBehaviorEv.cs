using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Delete
{
    class AcsActivityLogDeleteBehaviorEv : BeanObjectBase, IAcsActivityLogDelete
    {
        ACS_ACTIVITY_LOG entity;

        internal AcsActivityLogDeleteBehaviorEv(CommonParam param, ACS_ACTIVITY_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityLogDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsActivityLogDAO.Truncate(entity);
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
                result = result && AcsActivityLogCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
