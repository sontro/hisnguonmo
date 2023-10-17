using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Delete
{
    class SdaHideControlDeleteBehaviorEv : BeanObjectBase, ISdaHideControlDelete
    {
        SDA_HIDE_CONTROL entity;

        internal SdaHideControlDeleteBehaviorEv(CommonParam param, SDA_HIDE_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.Truncate(entity);
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
                result = result && SdaHideControlCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
