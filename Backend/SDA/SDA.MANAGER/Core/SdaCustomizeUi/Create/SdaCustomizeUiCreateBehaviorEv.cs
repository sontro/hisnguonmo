using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Create
{
    class SdaCustomizeUiCreateBehaviorEv : BeanObjectBase, ISdaCustomizeUiCreate
    {
        SDA_CUSTOMIZE_UI entity;

        internal SdaCustomizeUiCreateBehaviorEv(CommonParam param, SDA_CUSTOMIZE_UI data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCustomizeUiCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCustomizeUiDAO.Create(entity);
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
                result = result && SdaCustomizeUiCheckVerifyValidData.Verify(param, entity);
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
