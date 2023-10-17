using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Create
{
    class SdaHideControlCreateBehaviorEv : BeanObjectBase, ISdaHideControlCreate
    {
        SDA_HIDE_CONTROL entity;

        internal SdaHideControlCreateBehaviorEv(CommonParam param, SDA_HIDE_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaHideControlDAO.Create(entity);
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
