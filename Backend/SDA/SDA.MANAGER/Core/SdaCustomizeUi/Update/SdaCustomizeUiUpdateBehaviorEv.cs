using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Update
{
    class SdaCustomizeUiUpdateBehaviorEv : BeanObjectBase, ISdaCustomizeUiUpdate
    {
        SDA_CUSTOMIZE_UI current;
        SDA_CUSTOMIZE_UI entity;

        internal SdaCustomizeUiUpdateBehaviorEv(CommonParam param, SDA_CUSTOMIZE_UI data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCustomizeUiUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCustomizeUiDAO.Update(entity);
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
                result = result && SdaCustomizeUiCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
