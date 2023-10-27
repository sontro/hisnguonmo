using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Delete
{
    class AcsControlDeleteBehaviorEv : BeanObjectBase, IAcsControlDelete
    {
        ACS_CONTROL entity;

        internal AcsControlDeleteBehaviorEv(CommonParam param, ACS_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlDAO.Truncate(entity);
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
                result = result && AcsControlCheckVerifyIsUnlock.Verify(param, entity.ID);
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
