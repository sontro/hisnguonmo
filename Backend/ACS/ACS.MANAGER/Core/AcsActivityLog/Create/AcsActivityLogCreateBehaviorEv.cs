using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Create
{
    class AcsActivityLogCreateBehaviorEv : BeanObjectBase, IAcsActivityLogCreate
    {
        ACS_ACTIVITY_LOG entity;

        internal AcsActivityLogCreateBehaviorEv(CommonParam param, ACS_ACTIVITY_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityLogCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsActivityLogDAO.Create(entity);
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
