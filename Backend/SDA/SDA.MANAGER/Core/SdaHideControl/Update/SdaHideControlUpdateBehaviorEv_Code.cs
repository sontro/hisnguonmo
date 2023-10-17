using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaHideControl.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Update
{
    class SdaHideControlUpdateBehaviorEv : BeanObjectBase, ISdaHideControlUpdate
    {
        SDA_HIDE_CONTROL current;
        SDA_HIDE_CONTROL entity;

        internal SdaHideControlUpdateBehaviorEv(CommonParam param, SDA_HIDE_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.Update(entity);
                if (result) { SdaHideControlEventLogUpdate.Log(current, entity); }
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
                result = result && SdaHideControlCheckVerifyValidData.Verify(param, entity);
                result = result && SdaHideControlCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaHideControlCheckVerifyExistsCode.Verify(param, entity.HIDE_CONTROL_CODE, entity.ID);
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
