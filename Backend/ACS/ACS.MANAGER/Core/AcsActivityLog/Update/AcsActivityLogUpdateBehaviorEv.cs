using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Update
{
    class AcsActivityLogUpdateBehaviorEv : BeanObjectBase, IAcsActivityLogUpdate
    {
        ACS_ACTIVITY_LOG current;
        ACS_ACTIVITY_LOG entity;

        internal AcsActivityLogUpdateBehaviorEv(CommonParam param, ACS_ACTIVITY_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityLogUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsActivityLogDAO.Update(entity);
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
                result = result && AcsActivityLogCheckVerifyValidData.Verify(param, entity);
                result = result && AcsActivityLogCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
